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
namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "Card")]
    public class ExplosionBullet : BulletEffect
    {
        [SerializeField] private float enemyHitExplotionRadius = 1;
        [SerializeField] private float surfaceHitExplosionRadius = 0.5f;
        private LayerMask enemyLayer;
        [SerializeField] private GameObject explosionPrefab;

        public override void Initialize(){
            enemyLayer = LayerMask.NameToLayer("Enemy");
        }
        public override void OnEnemyHit(EnemyTakeDamage enemy, float damage)
        {
            Ray enemyOrgin = new Ray(enemy.transform.position, enemy.transform.forward);
            RaycastHit[] hits = Physics.SphereCastAll(enemyOrgin, enemyHitExplotionRadius, 0.1f, enemyLayer);
            for (int i = 1; i < hits.Length; i++) 
            {
                if (hits[i].collider.gameObject.TryGetComponent(out EnemyTakeDamage enemyDmg))
                {
                    enemyDmg.TakeDamage(damage * DamageMultiplier);
                    Debug.Log(hits[i].collider.gameObject.name + " took " + (damage * DamageMultiplier) + " damage");
                }
            }

            GameObject explosion = Instantiate(explosionPrefab, enemy.transform.position, Quaternion.identity);
            explosion.transform.localScale = Vector3.one * enemyHitExplotionRadius;
            explosion.GetComponent<DestroyObjectAfterSeconds>().Destroy(0.3f);
        }
        //corutine is tempory and should be removed when we add an animation

        public override void OnHitOther(Vector3 point, float damage)
        {
            RaycastHit[] hits = Physics.SphereCastAll(point, surfaceHitExplosionRadius, Vector3.zero,0.1f, enemyLayer);
            for (int i = 1; i < hits.Length; i++)
            {
                if(hits[i].collider.gameObject.TryGetComponent(out EnemyTakeDamage enemy))
                {
                    enemy.TakeDamage(damage * DamageMultiplier);
                    Debug.Log(hits[i].collider.gameObject.name + " took " + (damage * DamageMultiplier) + " damage");
                }
            }

            GameObject explosion = Instantiate(explosionPrefab, point, Quaternion.identity);
            explosion.transform.localScale = Vector3.one * surfaceHitExplosionRadius;
            explosion.GetComponent<DestroyObjectAfterSeconds>().Destroy(0.3f);
        }
    }
}
