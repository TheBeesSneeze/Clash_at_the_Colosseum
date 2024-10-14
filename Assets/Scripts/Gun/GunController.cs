/*******************************************************************************
* File Name :         GunController.cs
* Author(s) :         Toby, Alec
* Creation Date :     3/20/2024
*
* Brief Description : 
 *****************************************************************************/

using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class GunController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] public ShootingMode shootingMode;
    [SerializeField] public GameObject BulletPrefab;
    [SerializeField][Min(1)] private int shotsTillCoolDown;
    [SerializeField][Min(0)] private float overheatCoolDown;
    
    [Header("Unity Stuff")]
    public Transform bulletSpawnPoint;

    public List<BulletEffect> bulletEffects;
    private LayerMask scanMask;

    [HideInInspector] public float secondsSinceLastShoot;
    private Rigidbody playerRB;
    private Camera playerCamera;
    private Animator animator;

    private bool shootHeld;
    private int currentShots;
    private float cooldown;
    private bool isOverHeating;

    private void Start()
    {
        DebugStartingBulletEffects();
        //InputEvents.Instance.ShootHeld.AddListener(ShootHeld);
        playerRB = GetComponent<Rigidbody>();
        playerCamera = Camera.main;
        animator = GetComponent<Animator>();
        if(SaveData.SelectedGun!= null)
            shootingMode = SaveData.SelectedGun;
        LoadShootingMode(shootingMode);
        InputEvents.Instance.ShootStarted.AddListener(OnShootStart);
        //InputEvents.Instance.ShootCanceled.AddListener(ShootReleased);
        scanMask |= (1 << LayerMask.GetMask("Default"));
        scanMask |= (1 << LayerMask.GetMask("Enemy"));
        currentShots = 0;
        cooldown = 0;
    }

    public void DebugStartingBulletEffects()
    {
        List<BulletEffect> newBulletEffects=new List<BulletEffect>();
        foreach(BulletEffect bulletEffect in bulletEffects)
        {
            newBulletEffects.Add((BulletEffect)Instantiate(bulletEffect));
        }
        bulletEffects = newBulletEffects;
    }

    /// <summary>
    /// call this in from those little pickup guys
    /// </summary>
    public void LoadShootingMode(ShootingMode shootMode)
    {
        //fuck you im going violent
        if(shootMode == null)
        {
            Debug.LogError("empty shooting mode");
            Application.Quit(); // this is so extreme lol i love this
        }
        shootingMode = shootMode;
        shootHeld = true; 
    }
    public void AddBulletEffect(BulletEffect bulletEffect)
    {
        if (bulletEffects == null)
            bulletEffects = new List<BulletEffect>();
        
        if(bulletEffect == null)
        {
            Debug.LogError("what the fuck bro");
            Application.Quit();
            return;
        }
        //BulletEffect copy = Instantiate(bulletEffect);
        //bulletEffects.Add(copy);
        bulletEffects.Add(bulletEffect);
        PublicEvents.OnUpgradeReceived?.Invoke(bulletEffect);
        BulletPoolManager.AddPlayerBulletEffect(bulletEffect);
    }
    /// <summary>
    /// shoots all the bullets. calls the OnBulletShoot function
    /// </summary>
    private void Shoot()
    {
        if(currentShots < shotsTillCoolDown)
        {
            secondsSinceLastShoot = 0;

            for (int i = 0; i < shootingMode.BulletsPerShot; i++)
            {
                ShootBullet();
            }
            ++currentShots;
            PublicEvents.OnPlayerShoot.Invoke();
        }
            
        
        //playerRB.AddForce(-playerCamera.transform.forward * shootingMode.RecoilForce, ForceMode.Impulse);
    }

    /// <summary>
    /// direction is (get this) the direction the enemyBullet goes
    /// </summary>
    private void ShootBullet()
    {
        //alec put code here
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(.5f, 0.5f, 0f));
        Vector3 destination;
        if(Physics.Raycast(ray, out RaycastHit hit, 1000f, scanMask))
        {
            destination = hit.point;
        }
        else
        {
            destination = ray.GetPoint(100f);
        }

        destination += new Vector3(
            Random.Range(-shootingMode.BulletAccuracyOffset, shootingMode.BulletAccuracyOffset),
            Random.Range(-shootingMode.BulletAccuracyOffset, shootingMode.BulletAccuracyOffset),
            Random.Range(-shootingMode.BulletAccuracyOffset, shootingMode.BulletAccuracyOffset));
        Vector3 dir = destination - bulletSpawnPoint.position;

        //var enemyBullet = Instantiate(BulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        GameObject bullet = BulletPoolManager.Instantiate(bulletSpawnPoint.position);
        if (bullet == null)
        {
            return;
        }
        bullet.transform.forward = dir.normalized;
        var bulletObj = bullet.GetComponent<Bullet>();
        bulletObj.damageAmount = shootingMode.BulletDamage;
        bulletObj.bulletForce = shootingMode.BulletSpeed;
        bulletObj.GetComponent<Rigidbody>().velocity = playerRB.GetPointVelocity(bulletSpawnPoint.position);
        bulletObj.OnBulletShoot(dir);
    }
    
    private void Update()
    {
        //DebugTarget();
        secondsSinceLastShoot += Time.deltaTime;
        cooldown -= Time.deltaTime;

        if(cooldown <= 0f && isOverHeating)
        {
            currentShots = 0;
            isOverHeating = false;
        }
        else if (cooldown <= 0f)
        {
            if (currentShots >= shotsTillCoolDown)
            {
                cooldown = overheatCoolDown;
                isOverHeating = true;
                return; 
            }
        }
        

        if (!InputEvents.ShootPressed) return;
        if (secondsSinceLastShoot < (1/shootingMode.ShotsPerSecond)) return;

        if (!shootingMode.CanHoldFire) return;

        //shootin time
        
        Shoot();
        
        
        
    }

    private void OnShootStart()
    {
        Shoot();
    }
    private void DebugTarget()
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(.5f, 0.5f, 0f));
        Vector3 destination;
        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, LayerMask.GetMask("Default")))
        {
            destination = hit.point;
        }
        else
        {
            destination = ray.GetPoint(1000f);
        }
        Debug.DrawLine(ray.origin, destination, Color.red);
        Debug.DrawLine(bulletSpawnPoint.position, destination, Color.red);
    }

    public int GetShotsLeft()
    {
        if(isOverHeating)
            return 0;

        return shotsTillCoolDown - currentShots;
    }

    public int GetShotsTillCoolDown()
    {
        return shotsTillCoolDown;
    }

    

    

    
}
