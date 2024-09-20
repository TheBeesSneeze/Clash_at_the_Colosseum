/*******************************************************************************
* File Name :         ExplosionBullet.cs
* Author(s) :         Tyler B, Toby
* Creation Date :     3/25/2024
*
* Brief Description : Spawns a funny lil explosion guy when it blows up
 *****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "Card")]
    public class ExplosionBullet : BulletEffect
    {
        [SerializeField] private float explotionRadius = 1;
        private LayerMask enemyLayer = LayerMask.GetMask("Enemy");


        public override void Initialize(){}
        public override void OnEnemyHit(EnemyTakeDamage enemy, float damage)
        {
            Ray enemyOrgin = new Ray(enemy.transform.position, enemy.transform.forward);
            RaycastHit[] hits = Physics.SphereCastAll(enemyOrgin, explotionRadius, 0, enemyLayer);
            Gizmos.DrawWireSphere(enemy.transform.position, explotionRadius);
            Debug.Log(hits.Length);
            foreach (RaycastHit hit in hits) {
                Debug.Log(hit.collider.gameObject.name);
            }
        }
        public override void OnHitOther(Vector3 point, float damage)
        {
            //still does an explotion if it hits a wall
        }
    }
}
