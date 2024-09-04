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
using static AudioManager;
using static Unity.VisualScripting.Member;

public class GunController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] public ShootingMode shootingMode;
    [SerializeField] private GameObject BulletPrefab;
    
    [Header("Unity Stuff")]
    public Transform bulletSpawnPoint;

    [ReadOnly] public BulletEffect bulletEffect1;
    [ReadOnly] public BulletEffect bulletEffect2;
    private LayerMask scanMask;

    private float secondsSinceLastShoot;
    private Rigidbody playerRB;
    private Camera playerCamera;
    private Animator animator;

    private bool canShoot;

    /// <summary>
    /// call this in from those little pickup guys
    /// </summary>
    public void LoadShootingMode(ShootingMode shootMode)
    {
        //fuck you im going violent
        if(shootMode == null)
        {
            Debug.LogError("empty shooting mode");
            Application.Quit();
        }

        shootingMode = shootMode;

        //change color
        //Gun.GetChild(0).GetComponent<Renderer>().material.color = shootMode.GunColor;

        canShoot = true;
    }

    /// <summary>
    /// shoots all the bullets. calls the ShootBullet function
    /// </summary>
    private void Shoot()
    {
        Debug.Log("pew");
        secondsSinceLastShoot = 0;
        //AudioManager.instance.Play("Shoot Default");

        for (int i = 0; i < shootingMode.BulletsPerShot; i++)
        {
            ShootBullet();
        }

        //playerRB.AddForce(-playerCamera.transform.forward * shootingMode.RecoilForce, ForceMode.Impulse);
    }

    /// <summary>
    /// direction is (get this) the direction the bullet goes
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

        //var bullet = Instantiate(BulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        GameObject bullet = BulletPoolManager.GetPooledObject();
        if (bullet == null)
        {
            return;
        }
        Debug.Log("setting active rn");
        bullet.transform.position = bulletSpawnPoint.position;
        bullet.transform.forward = dir.normalized;
        var bulletObj = bullet.GetComponent<Bullet>();
        bulletObj.damageAmount = shootingMode.BulletDamage;
        bulletObj.bulletForce = shootingMode.BulletSpeed;
        bulletObj.GetComponent<Rigidbody>().velocity = playerRB.GetPointVelocity(bulletSpawnPoint.position);
        bulletObj.Initialize(bulletEffect1, bulletEffect2, dir);
    }
    
    private void Update()
    {
        DebugTarget();
        secondsSinceLastShoot += Time.deltaTime;

        if (!canShoot) return;
        if (!InputEvents.ShootPressed) return;
        if (secondsSinceLastShoot < (1/shootingMode.ShotsPerSecond)) return;

        if (!shootingMode.CanHoldFire)
            canShoot = false;

        //shootin time
        Shoot();
    }

    private void OnShootStart()
    {
        canShoot = true;
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

    private void Start()
    {
        //InputEvents.Instance.ShootHeld.AddListener(ShootHeld);
        //InputEvents.Instance.ShootCanceled.AddListener(ShootReleased);
        playerRB = GetComponent<Rigidbody>();
        playerCamera = Camera.main;
        animator = GetComponent<Animator>();
        LoadShootingMode(shootingMode);
        InputEvents.Instance.ShootStarted.AddListener(OnShootStart);
        scanMask |= (1 << LayerMask.GetMask("Default"));
        scanMask |= (1 << LayerMask.GetMask("Enemy"));
    }

    
}
