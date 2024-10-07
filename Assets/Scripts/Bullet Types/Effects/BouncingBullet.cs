/*******************************************************************************
* File Name :         BouncingBullet.cs
* Author(s) :         Tyler B, Toby
* Creation Date :     10/1
*
* Brief Description : bullets bounce off walls to the closest enemy if the enemy 
*                     is in dflection range
 *****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "BouncingBullet", menuName = "BulletEffects/Bouncing")]
    public class BouncingBullet : BulletEffect
    {
        [SerializeField] private bool TargetEnemies = true;
        [SerializeField] private bool BounceOffSurfaces = true;

        [Tooltip("the radius of a sphere around where the bullet makes contact with a surface. if there are enemies in this range it should try and deflect a bullet in that direction")]
        [SerializeField] private float deflectionAimRange = 100;

        [Tooltip("number of times it can bounce off the wall before it gets destroyed")]
        [SerializeField] private int BouncesUntilDestroy = 10;
        [Tooltip("Bullet gets slower each hit. set to 1 for same speed.")]
        [SerializeField] private float SlowdownMultiplier=0.5f;
        [Tooltip("Make the bullet last longer if u want")]
        [SerializeField] private float OverrideBulletTime=1;

        private int bounces;
        private Rigidbody rb;
        private LayerMask enemyLayer;

        public override void Initialize(Bullet bullet) 
        {
            //DestroyBulletOnSurfaceContact = false; // this can be set on the scriptable object
            rb = bullet.GetComponent<Rigidbody>();
            bullet.despawnTime = Mathf.Max(OverrideBulletTime, bullet.despawnTime);
            bounces = BouncesUntilDestroy;
            enemyLayer = LayerMask.NameToLayer("Enemy");

            Debug.Log("hello!");
        }
        public override void OnEnemyHit(EnemyTakeDamage type, float damage, Bullet bullet)
        {
            // pretty sure this shouldn't do anything special for this bullet effect
        }
        public override void OnHitOther(RaycastHit hit, float damage, Bullet bullet)
        {
            Debug.Log(bounces);
            //figures out how many bounces the bullet has left
            if (bounces <= 0)
            {
                //BulletPoolManager.Destroy(parentBullet);
                //return;
            }
            bounces--;

            if (TargetEnemies)
            {
                if (TargetEnemy(hit.point, bullet))
                {
                    Debug.Log("target enemy");
                    return;
                }
            }

            if(BounceOffSurfaces)
            {
                Debug.Log("bounce");
                Bounce(hit);
            }
        }

        /// <summary>
        /// return true if it targeted enemy
        /// </summary>
        /// <returns></returns>
        private bool TargetEnemy(Vector3 center, Bullet bullet)
        {
            //makes a list of colliders of all the enemies in the deflectionAimRange
            Collider[] enemyColliders = Physics.OverlapSphere(center, deflectionAimRange, enemyLayer);

            if (enemyColliders.Length <= 0)
                return false;

            foreach(Collider collider in enemyColliders)
            {
                if(collider.TryGetComponent<EnemyTakeDamage>(out EnemyTakeDamage enemy))
                {
                    Vector3 direction = enemy.transform.position - bullet.transform.position;
                    rb.velocity = direction.normalized * rb.velocity.magnitude;
                    return true;
                }
            }
            return false;
        }
        private void Bounce(RaycastHit hit)
        {
            Debug.Log(rb.velocity);
            Vector3 direction = Vector3.Reflect(rb.velocity, hit.normal);
            rb.velocity = direction.normalized * rb.velocity.magnitude;
            Debug.Log(rb.velocity);
        }
    }
}
