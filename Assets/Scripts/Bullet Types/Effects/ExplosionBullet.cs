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
        [SerializeField] private LayerMask enemyLayer;
        [SerializeField] private GameObject explosion;

        public override void Initialize(){}
        public override void OnEnemyHit(EnemyTakeDamage enemy, float damage)
        {
            Ray enemyOrgin = new Ray(enemy.transform.position, enemy.transform.forward);
            RaycastHit[] hits = Physics.SphereCastAll(enemyOrgin, explotionRadius, 0, enemyLayer);
            Instantiate(explosion, enemy.transform.position, Quaternion.identity);
            Debug.Log(hits.Length + " enemies hit");
            for (int i = 1; i < hits.Length; i++) {
                hits[i].collider.gameObject.GetComponent<EnemyTakeDamage>().TakeDamage(damage*DamageMultiplier);
                Debug.Log(hits[i].collider.gameObject.name + " took " + (damage*DamageMultiplier) + " damage");
            }
        }
        //corutine is tempory and should be removed when we add an animation

        public override void OnHitOther(Vector3 point, float damage)
        {
            //still does an explotion if it hits a wall
        }
    }
}
