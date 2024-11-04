/*******************************************************************************
* File Name :         GunController.cs
* Author(s) :         Toby, Alec, Clare
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

    //returns true if the gun is reloading
    private bool isOverHeating;
    private int shotsTillCoolDown;
    private bool canInfinteShoot;
    [HideInInspector] public float overheatCoolDown;
    

    private void Start()
    {
        DebugStartingBulletEffects();
        InputEvents.Instance.PauseStarted.AddListener(ShootStopped);
        playerRB = GetComponent<Rigidbody>();
        playerCamera = Camera.main;
        animator = GetComponent<Animator>();
        if(SaveData.SelectedGun!= null)
        shootingMode = SaveData.SelectedGun;
        LoadShootingMode(shootingMode);
        InputEvents.Instance.ShootStarted.AddListener(OnShootStart);
        scanMask |= (1 << LayerMask.GetMask("Default"));
        scanMask |= (1 << LayerMask.GetMask("Enemy"));
        currentShots = 0;
        cooldown = 0;
        shotsTillCoolDown = shootingMode.ClipSize;
        overheatCoolDown = shootingMode.ReloadSpeed;
        canInfinteShoot = shootingMode.canInfiniteFire;
        InputEvents.Instance.ReloadStarted.AddListener(Reload);
           
    }
    public void ShootStopped()
    {
        shootHeld = false;
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
            Application.Quit(); //this is so extreme lol i love this
        }
        shootingMode = shootMode;
        shootHeld = true; 
    }
    public void AddBulletEffect(BulletEffect bulletEffect, bool save=true)
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

        if (save)
        {
            if(SaveData.bulletEffectPool.Contains(bulletEffect)) SaveData.bulletEffectPool.Remove(bulletEffect);
            SaveData.gotBulletEffects.Add(bulletEffect);
        }
    }
    /// <summary>
    /// shoots all the bullets. calls the OnBulletShoot function
    /// </summary>
    
    //Clare made this function
    private void Shoot()
    {
        if (currentShots < shotsTillCoolDown)
        {
            secondsSinceLastShoot = 0;

            for (int i = 0; i < shootingMode.BulletsPerShot; i++)
            {
                ShootBullet();
            }
            ++currentShots;
            PublicEvents.OnPlayerShoot.Invoke();
        }
        else if(canInfinteShoot)
        {
            secondsSinceLastShoot = 0;
            ShootBullet();
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

        //clare's if else stuff
        if (cooldown <= 0f && isOverHeating)
        {
            currentShots = 0;
            isOverHeating = false;
            PublicEvents.OnPlayerReload.Invoke();
        } else if (cooldown <= 0f) {
            if (currentShots >= shotsTillCoolDown && !canInfinteShoot)
            {
                Reload();
            }
        }


        if (!InputEvents.ShootPressed) return;
        if (secondsSinceLastShoot < (1/shootingMode.ShotsPerSecond)) return;

        if (!shootingMode.CanHoldFire) return;

        //shootin time
        if (shootHeld) {
            Shoot();
        }
    }

    //Clare function
    private void Reload()
    {
        if (!isOverHeating) {
            currentShots = shotsTillCoolDown;
            cooldown = overheatCoolDown;
            isOverHeating = true;
            PublicEvents.Reloading.Invoke();
            return;
        }
    }

    private void OnShootStart()
    {
        Shoot();
        shootHeld = true;
    }
    private void DebugTarget()
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(.5f, 0.5f, 0f));
        Vector3 destination;
        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, LayerMask.GetMask("Default")))
        {
            destination = hit.point;
        } else {
            destination = ray.GetPoint(1000f);
        }
        Debug.DrawLine(ray.origin, destination, Color.red);
        Debug.DrawLine(bulletSpawnPoint.position, destination, Color.red);
    }

    // god i hate public variables and i love getters

    public int GetShotsLeft()
    {
        if(isOverHeating)
            return 0;

        return shotsTillCoolDown - currentShots;
    }

    public int GetShotsFired()
    {
        //if (isOverHeating) return 0;

        return currentShots;
    }

    public int GetShotsTillCoolDown()
    {
        return shotsTillCoolDown;
    }

    /// <summary>
    /// 0 <-> 1 
    /// 0 is no shots left
    /// 1 is full ammo
    /// </summary>
    public float GetShotsLeftPercent()
    {
        if (isOverHeating)
            return 0;

        return (float)(shotsTillCoolDown - currentShots) / (float) shotsTillCoolDown;
    }
    

    

    
}
