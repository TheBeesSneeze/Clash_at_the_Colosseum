/*******************************************************************************
* File Name :         ExplosionBullet.cs
* Author(s) :         Toby Schamberger
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
        
        public GameObject ExplosionPrefab;

        public override void Initialize()
        {
        }
        public override void OnEnemyHit(EnemyTakeDamage type, float damage)
        {
            Debug.LogWarning("old code. pls update");
            GameObject explosion = Instantiate(ExplosionPrefab, type.transform.position, ExplosionPrefab.transform.rotation);

            //explosion.GetComponent<AttackType>().Damage = damage * DamageMultiplier;
        }

        public override void OnHitOther(Vector3 point, float damage)
        {
            Debug.LogWarning("old code. pls update");
            GameObject explosion = Instantiate(ExplosionPrefab, point, ExplosionPrefab.transform.rotation);

            //explosion.GetComponent<AttackType>().Damage = damage * DamageMultiplier;
        }
    }
}
