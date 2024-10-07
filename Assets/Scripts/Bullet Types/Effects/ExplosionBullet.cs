/*******************************************************************************
* File Name :         ExplosionBullet.cs
* Author(s) :         Tyler B, Toby
* Creation Date :     3/25/2024
*
* Brief Description : Spawns a funny lil explosionPrefab guy when it blows up
 *****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;
namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "Card")]
    public class ExplosionBullet : BulletEffect
    {
        [SerializeField] private float enemyHitExplotionRadius = 1;
        [SerializeField] private float surfaceHitExplosionRadius = 0.5f;
        [SerializeField] private LayerMask enemyLayer;
        [SerializeField] private GameObject explosionPrefab;

        [SerializeField] private bool ExplodeOnEnemyHit = false;
        [SerializeField] private bool ExplodeOnSurfaceHit = false;
        [SerializeField] private bool ExplodeOnDestroy = true;

        public override void OnShoot(Bullet bullet){
            enemyLayer = LayerMask.NameToLayer("Enemy");
        }
        public override void OnEnemyHit(EnemyTakeDamage enemy, float damage, Bullet bullet)
        {
            if (!ExplodeOnEnemyHit) return;

            Ray enemyOrgin = new Ray(enemy.transform.position, enemy.transform.forward);
            RaycastHit[] hits = Physics.SphereCastAll(enemyOrgin, enemyHitExplotionRadius, 0.1f, enemyLayer);
            for (int i = 1; i < hits.Length; i++) 
            {
                hits[i].collider.GetComponent<EnemyTakeDamage>().TakeDamage(damage * DamageMultiplier);
                //Debug.Log(hits[i].collider.gameObject.name + " took " + (damage * DamageMultiplier) + " damage");
            }
            GameObject explosion = Instantiate(explosionPrefab, enemy.transform.position, Quaternion.identity);
            explosion.transform.localScale = Vector3.one * enemyHitExplotionRadius * 2;
            explosion.GetComponent<DestroyObjectAfterSeconds>().DestroyTimer(0.3f);
        }
        public override void OnHitOther(RaycastHit hit, float damage, Bullet bullet)
        {
            if(!ExplodeOnSurfaceHit) return;    

            RaycastHit[] hits = Physics.SphereCastAll(hit.point, surfaceHitExplosionRadius, Vector3.zero,0.1f, enemyLayer);
            for (int i = 1; i < hits.Length; i++)
            {
                hits[i].collider.GetComponent<EnemyTakeDamage>().TakeDamage(damage * DamageMultiplier);
                //Debug.Log(hits[i].collider.gameObject.name + " took " + (damage * DamageMultiplier) + " damage");
            }
            GameObject explosion = Instantiate(explosionPrefab, hit.point, Quaternion.identity);
            explosion.transform.localScale = Vector3.one * surfaceHitExplosionRadius * 2;
            explosion.GetComponent<DestroyObjectAfterSeconds>().DestroyTimer(0.3f);
        }

        public override void OnDestroyBullet(Bullet bullet, float damage)
        {
            if(!ExplodeOnDestroy) return;

            Collider[] enemyColliders = Physics.OverlapSphere(bullet.transform.position, surfaceHitExplosionRadius, enemyLayer);
            foreach(Collider collider in enemyColliders)
            {
                if(collider.TryGetComponent<EnemyTakeDamage>(out EnemyTakeDamage enemy))
                {
                    enemy.TakeDamage(damage * DamageMultiplier);
                }
            }
            GameObject explosion = Instantiate(explosionPrefab, bullet.transform.position, Quaternion.identity);
            explosion.transform.localScale = Vector3.one * surfaceHitExplosionRadius * 2;
            explosion.GetComponent<DestroyObjectAfterSeconds>().DestroyTimer(0.3f);
        }
    }
}
