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
using static UnityEngine.EventSystems.EventTrigger;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private float despawnTime = 5f;
    [SerializeField] private LayerMask hitLayers;
    [SerializeField] private bool DealPlayerDamage = true;
    [SerializeField] private bool DealEnemyDamage = true;

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
    public void Initialize(Vector3 dir)
    {
        rb.AddForce(dir.normalized * bulletForce, ForceMode.Impulse);
        lastPosition = transform.position;


        SetColorGradient();
    }

    public void Initialize(List<BulletEffect> effects, Vector3 dir)
    {
        rb.AddForce(dir.normalized * bulletForce, ForceMode.Impulse);
        lastPosition = transform.position;

        if (effects == null) return;
        if (effects.Count <= 0)return;

        for (int i = 0; i < gunController.bulletEffects.Count; i++)
        {
            gunController.bulletEffects[i].DefaultInitialize(this, gunController);
        }

        SetColorGradient();
    }

    public void ResetBullet()
    {
        lastPosition = transform.position;
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
        
        OnHitSurface(hit.point);
    }

    private void OnEnemyHit(EnemyTakeDamage enemy)
    {
        enemy.TakeDamage(damageAmount);
        foreach(BulletEffect effect in gunController.bulletEffects)
        {
            effect.OnEnemyHit(enemy, damageAmount);
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
        for (int i=0; i< gunController.bulletEffects.Count; i++)
        {
            gunController.bulletEffects[i].OnHitOther(point, damageAmount);
        }

        if (DestroyOnSurfaceHit())
            BulletPoolManager.Destroy(this);

    }

    private bool DestroyOnEntityHit()
    {
        bool destroy = true;

        foreach (BulletEffect effect in gunController.bulletEffects)
        {
            if (!effect.DestroyBulletOnEntityContact)
                destroy = false;
        }

        return destroy;
    }

    private bool DestroyOnSurfaceHit()
    {
        bool destroy = true;

        foreach (BulletEffect effect in gunController.bulletEffects)
        {
            if (!effect.DestroyBulletOnSurfaceContact)
                destroy = false;
        }

        return destroy;
    }

    /// <summary>
    /// this code does not work
    /// </summary>
    private void SetColorGradient() //or does it???
    {
        //Debug.Log("todo: color gradient");
        /*
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
        */
    }

    /// <summary>
    /// averages the colors from both enemyBullet effects.
    /// returns white if no upgrades are loaded
    /// </summary>
    /// <returns></returns>
    private Color GetBulletColor()
    {
        return Color.white;
        /*
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
        */
    }

}
