/*******************************************************************************
* File Name :         Bullet.cs
* Author(s) :         Alec Pizziferro
* Creation Date :     3/22/2024
*
* Brief Description : Projectile Bullet Physics
 *****************************************************************************/
using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using static AudioManager;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private float despawnTime = 5f;
    [SerializeField] private LayerMask hitLayers;
    [SerializeField] private bool DealPlayerDamage = true;
    [SerializeField] private bool DealEnemyDamage = true;

    [HideInInspector] public BulletEffect _bulletEffect1;
    [HideInInspector] public BulletEffect _bulletEffect2;

    private Rigidbody rb;
    private Vector3 lastPosition;
    private float timeActive;
    private GunController gunController;

    [HideInInspector] public float damageAmount = 10f;
    [HideInInspector] public float bulletForce = 200f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        gunController = GameObject.FindObjectOfType<GunController>();
    }

    /// <summary>
    /// was previously start function, changed to get called in GunController
    /// </summary>
    public void Initialize(BulletEffect bulletEffect1, BulletEffect bulletEffect2, Vector3 dir)
    {
        rb.AddForce(dir.normalized * bulletForce, ForceMode.Impulse);
        lastPosition = transform.position;

        _bulletEffect1 = bulletEffect1;
        _bulletEffect2 = bulletEffect2;

        if (_bulletEffect1 != null)
        {
            _bulletEffect1.DefaultInitialize(this, gunController);
        }

        SetColorGradient();
    }

    public void ResetBullet()
    {

        lastPosition = transform.position;
        _bulletEffect1 = null;
        _bulletEffect2 = null;
        timeActive = 0;
    }

    private void FixedUpdate()
    {
        if (!gameObject.activeInHierarchy)
            return;

        if (timeActive >= despawnTime)
        {
            BulletPoolManager.Destroy(this);
            return;
        }
        timeActive += Time.fixedDeltaTime;
        Debug.DrawRay(lastPosition, transform.forward);

        RaycastHit hit;
        if (!Physics.Raycast(lastPosition, transform.forward, out hit, Vector3.Distance(transform.position, lastPosition), hitLayers,
            QueryTriggerInteraction.Ignore))
        {
            lastPosition = transform.position;
            return;
        }
        Debug.Log("hit");

        if (DealEnemyDamage && hit.collider.TryGetComponent(out EnemyTakeDamage enemy ))
        {
            print("hit enemy");
            OnEnemyHit(enemy);
            return;
        }
        if (DealPlayerDamage && hit.collider.TryGetComponent(out PlayerBehaviour player))
        {
            print("hit player");
            OnPlayerHit(player);
            return;
        }
        //if hit something that isnt enemy
        
        Debug.Log("hit other");
        OnHitSurface(hit.point);
    }

    private void OnEnemyHit(EnemyTakeDamage enemy)
    {
        enemy.TakeDamage(damageAmount);
        if (_bulletEffect1 != null)
        {
            _bulletEffect1.OnEnemyHit(enemy, damageAmount);
        }

        if (_bulletEffect2 != null)
        {
            _bulletEffect2.OnEnemyHit(enemy, damageAmount);
        }

        if(DestroyOnEntityHit())
            BulletPoolManager.Destroy(this);
    }

    private void OnPlayerHit(PlayerBehaviour player)
    {

        player.TakeDamage(damageAmount);

        if (DestroyOnEntityHit())
            BulletPoolManager.Destroy(this);
    }

    private void OnHitSurface(Vector3 point)
    {
        if (_bulletEffect1 != null)
        {
            _bulletEffect1.OnHitOther(point, damageAmount);
        }

        if (_bulletEffect2 != null)
        {
            _bulletEffect2.OnHitOther(point, damageAmount);
        }
        
        if(DestroyOnSurfaceHit())
            BulletPoolManager.Destroy(this);

    }

    private bool DestroyOnEntityHit()
    {
        bool destroy = true;

        if (_bulletEffect1 != null)
        {
            if (!_bulletEffect1.DestroyBulletOnEntityContact)
                destroy = false;
        }
        if (_bulletEffect2 != null)
        {
            if (!_bulletEffect2.DestroyBulletOnEntityContact)
                destroy = false;
        }

        return destroy;
    }

    private bool DestroyOnSurfaceHit()
    {
        bool destroy = true;

        if (_bulletEffect1 != null)
        {
            if (!_bulletEffect1.DestroyBulletOnSurfaceContact)
                destroy = false;
        }
        if (_bulletEffect2 != null)
        {
            if (!_bulletEffect2.DestroyBulletOnSurfaceContact)
                destroy = false;
        }

        return destroy;
    }

    /// <summary>
    /// this code does not work
    /// </summary>
    private void SetColorGradient() //or does it???
    {
        TrailRenderer tr = GetComponent<TrailRenderer>();
        tr.enabled = true;

        tr.startColor = Color.white;
        tr.endColor = Color.white;

        if (_bulletEffect1 != null)
        {
            tr.startColor = _bulletEffect1.TrailColor;
        }
        if (_bulletEffect2 != null)
        {
            tr.endColor = _bulletEffect2.TrailColor;
        }
    }

    /// <summary>
    /// averages the colors from both enemyBullet effects.
    /// returns white if no upgrades are loaded
    /// </summary>
    /// <returns></returns>
    private Color GetBulletColor()
    {
        if (_bulletEffect1 == null && _bulletEffect2 == null)
        {
            return Color.white;
        }

        if (_bulletEffect1 == null)
        {
            return _bulletEffect2.TrailColor;
        }

        if (_bulletEffect2 == null)
        {
            return _bulletEffect1.TrailColor;
        }

        //weird way of averaging them but colors get weird when you add their parts to numbers above 1
        return (_bulletEffect1.TrailColor / 2) + (_bulletEffect2.TrailColor / 2);
    }

}
