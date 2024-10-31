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

    [SerializeField] public float despawnTime = 5f;
    [SerializeField] public float bulletGravity = 1f;
    [SerializeField] private LayerMask hitLayers;
    [SerializeField] private bool DealPlayerDamage = true;
    [SerializeField] private bool DealEnemyDamage = true;
    [SerializeField] private GameObject wallHitParticle; 

    private Rigidbody rb;
    private Vector3 lastPosition;
    private float timeActive;
    private GunController gunController;
    private List<BulletEffect> effects;
    
    [HideInInspector]public bool playerBullet = false;
    [HideInInspector] public float damageAmount = 10f;
    [HideInInspector] public float bulletForce = 200f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        gunController = GameObject.FindObjectOfType<GunController>();
        effects = new List<BulletEffect>();
    }

    /// <summary>
    /// only called for player bullets
    /// </summary>
    public void OneTimeInitalize(List<BulletEffect> bulletEffects)
    {
        foreach (BulletEffect effect in bulletEffects)
        {
            AddEffect(effect);
        }

    }

    /// <summary>
    /// was previously start function, changed to get called in GunController
    /// </summary>
    public void OnBulletShoot(Vector3 dir)
    {
        rb.AddForce(dir.normalized * bulletForce, ForceMode.Impulse);

        lastPosition = transform.position;

        if (effects == null)
            return; //who cares probably an enemy

        foreach (BulletEffect effect in effects)
        {
            effect.OnShoot(this);
        }
    }


    public void ResetBullet()
    {
        lastPosition = transform.position;
        timeActive = 0;
    }
    private void applyGravity() {
        Vector3 direction = new Vector3();
        direction.y = -bulletGravity;
        rb.AddForce(direction);
    }
    private void FixedUpdate()
    {
        applyGravity();
        if (!gameObject.activeInHierarchy)
            return;

        if (timeActive >= despawnTime)
        {
            if(playerBullet)
            {
                BulletPoolManager.Destroy(this);
                return;
            }
            Destroy();
            return;
        }
        timeActive += Time.fixedDeltaTime;
        Debug.DrawRay(lastPosition, transform.forward);
        transform.LookAt(transform.position + rb.velocity);

        RaycastHit hit;
        if (!Physics.Raycast(lastPosition, transform.forward, out hit, Vector3.Distance(transform.position, lastPosition), hitLayers,
            QueryTriggerInteraction.Ignore))
        {
            lastPosition = transform.position;
            return; // nothing happened
        }

        if (DealEnemyDamage && hit.collider.TryGetComponent(out EnemyTakeDamage enemy ))
        {
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
        
        OnHitSurface(hit);
    }


    /// <summary>
    /// Called in bulletpoolmanager
    /// </summary>
    public void AddEffect(BulletEffect effect)
    {
        if (effect == null)
            return ;
        if(effects == null)
            effects = new List<BulletEffect>();

        BulletEffect copy = Instantiate(effect);
        effects.Add(copy);
        copy.DefaultInitialize(this, gunController);
    }
   
    private void OnEnemyHit(EnemyTakeDamage enemy)
    {
        enemy.TakeDamage(damageAmount);
        foreach(BulletEffect effect in effects)
        {
            effect.OnEnemyHit(enemy, damageAmount, this);
        }

        if(DestroyOnEntityHit())
        {
            if (playerBullet)
            {
                BulletPoolManager.Destroy(this);
                return;
            }

            Destroy();
        }
            
    }

    private void OnPlayerHit(PlayerBehaviour player)
    {

        player.TakeDamage(damageAmount);

        if (DestroyOnEntityHit())
        {
            if (playerBullet)
            {
                BulletPoolManager.Destroy(this);
                return;
            }
            Destroy();
        }

    }

    private void OnHitSurface(RaycastHit hit)
    {
        for (int i=0; i< effects.Count; i++)
        {
            effects[i].OnHitOther(hit, damageAmount, this);
        }
        if (wallHitParticle != null)
            Instantiate(wallHitParticle, transform.position, Quaternion.identity);

        if (DestroyOnSurfaceHit())
        {
            if (playerBullet)
            {
                BulletPoolManager.Destroy(this);
                return;
            }
            Destroy();
        }



    }

    private bool DestroyOnEntityHit()
    {
        bool destroy = true;

        foreach (BulletEffect effect in effects)
        {
            if (!effect.DestroyBulletOnEntityContact)
                destroy = false;
        }

        return destroy;
    }

    private bool DestroyOnSurfaceHit()
    {
        bool destroy = true;

        foreach (BulletEffect effect in effects)
        {
            if (!effect.DestroyBulletOnSurfaceContact)
                destroy = false;
        }

        return destroy;
    }

    public void OnDisableBullet()
    {
        foreach(BulletEffect effect in effects)
        {
            effect.OnDestroyBullet(this, damageAmount);
        }
    }

    public void Destroy()
    {
        Destroy(this); 
    }

    /// <summary>
    /// averages the colors from both enemyBullet effects.
    /// returns white if no upgrades are loaded
    /// </summary>
    /// <returns></returns>

}
