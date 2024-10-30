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
            //enemyLayer = LayerMask.NameToLayer("Enemy");
        }
        public override void OnEnemyHit(EnemyTakeDamage enemy, float damage, Bullet bullet)
        {
            //if (!ExplodeOnEnemyHit) return;
            bool firstEnemy = true;
            Collider[] hits = Physics.OverlapSphere(enemy.transform.position, enemyHitExplotionRadius, enemyLayer);
            for (int i = 1; i < hits.Length; i++) 
            {
                if (firstEnemy)
                {
                    hits[i].GetComponent<EnemyTakeDamage>().TakeDamage(damage);
                    Debug.Log(hits[i].gameObject.name + " took " + (damage) + " damage");
                    firstEnemy = false;
                }
                else
                {
                    hits[i].GetComponent<EnemyTakeDamage>().TakeDamage(damage * DamageMultiplier);
                    Debug.Log(hits[i].gameObject.name + " took " + (damage * DamageMultiplier) + " damage");
                }
            }
            GameObject explosion = Instantiate(explosionPrefab, enemy.transform.position, Quaternion.identity);
            explosion.transform.localScale = Vector3.one * enemyHitExplotionRadius * 2;
            explosion.GetComponent<DestroyObjectAfterSeconds>().DestroyTimer(0.3f);
        }
        public override void OnHitOther(RaycastHit hit, float damage, Bullet bullet)
        {
            //if(!ExplodeOnSurfaceHit) return;

            Collider[] hits = Physics.OverlapSphere(hit.point, enemyHitExplotionRadius, enemyLayer);
            for (int i = 0; i < hits.Length; i++)
            {
                hits[i].GetComponent<EnemyTakeDamage>().TakeDamage(damage * DamageMultiplier);
                Debug.Log(hits[i].gameObject.name + i + " took " + (damage * DamageMultiplier) + " damage");
            }
            GameObject explosion = Instantiate(explosionPrefab, hit.point, Quaternion.identity);
            explosion.transform.localScale = Vector3.one * surfaceHitExplosionRadius * 2;
            explosion.GetComponent<DestroyObjectAfterSeconds>().DestroyTimer(0.3f);
        }

        public override void OnDestroyBullet(Bullet bullet, float damage)
        {
            /*if(!ExplodeOnDestroy) return;

            Collider[] enemyColliders = Physics.OverlapSphere(bullet.transform.position, surfaceHitExplosionRadius, enemyLayer);
            bool firstEnemy = true;
            foreach(Collider collider in enemyColliders)
            {
                if(collider.TryGetComponent<EnemyTakeDamage>(out EnemyTakeDamage enemy))
                {
                    if (firstEnemy) {
                        enemy.TakeDamage(damage);
                        Debug.Log(enemy.gameObject.name + " took " + (damage) + " damage");
                        firstEnemy = false;
                    }
                    else {
                        enemy.TakeDamage(damage * DamageMultiplier);
                        Debug.Log(enemy.gameObject.name + " took " + (damage * DamageMultiplier) + " damage");
                    }
                }
            }
            GameObject explosion = Instantiate(explosionPrefab, bullet.transform.position, Quaternion.identity);
            explosion.transform.localScale = Vector3.one * surfaceHitExplosionRadius * 2;
            explosion.GetComponent<DestroyObjectAfterSeconds>().DestroyTimer(0.3f);*/
        }
    }
}
