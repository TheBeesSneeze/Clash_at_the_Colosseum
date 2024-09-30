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
using UnityEngine;
using Random = UnityEngine.Random;

public class GunController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] public ShootingMode shootingMode;
    [SerializeField] public GameObject BulletPrefab;
    
    [Header("Unity Stuff")]
    public Transform bulletSpawnPoint;

    public List<BulletEffect> bulletEffects;
    private LayerMask scanMask;

    [HideInInspector] public float secondsSinceLastShoot;
    private Rigidbody playerRB;
    private Camera playerCamera;
    private Animator animator;

    private bool shootHeld;

    private void Start()
    {
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
        {
            bulletEffects = new List<BulletEffect>();
        }
        if(bulletEffect == null)
        {
            Debug.LogError("what the fuck bro");
            Application.Quit();
            return;
        }
        bulletEffects.Add(bulletEffect);
    }
    /// <summary>
    /// shoots all the bullets. calls the ShootBullet function
    /// </summary>
    private void Shoot()
    {
        secondsSinceLastShoot = 0;

        for (int i = 0; i < shootingMode.BulletsPerShot; i++)
        {
            ShootBullet();
        }

        PublicEvents.OnPlayerShoot.Invoke();

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
        bulletObj.Initialize(bulletEffects, dir);
    }
    
    private void Update()
    {
        DebugTarget();
        secondsSinceLastShoot += Time.deltaTime;

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

    

    
}
