/*******************************************************************************
* File Name :         ExplosionBullet.cs
* Author(s) :         Tyler B
* Creation Date :     3/25/2024
*
* Brief Description : bounces off a wall twards the nearest enemy. basicly aim bot
 *****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "BouncingBullet", menuName = "BulletEffects/Bouncing")]
    public class BouncingBullet : BulletEffect
    {

        [SerializeField] private LayerMask enemyLayer;
        [SerializeField] private float cosestEnemyDetectionRaduis = 30;
        public override void Initialize() { }
        public override void OnEnemyHit(EnemyTakeDamage type, float damage)
        {
            // I dont think that this needs to do anything
        }
        public override void OnHitOther(Vector3 point, float damage)
        {
            Collider[] enemyHits = Physics.OverlapSphere(point, cosestEnemyDetectionRaduis, enemyLayer);
            GameObject explosion = Instantiate(explosionPrefab, point, Quaternion.identity);
            explosion.transform.localScale = Vector3.one * surfaceHitExplosionRadius * 2;
            explosion.GetComponent<DestroyObjectAfterSeconds>().Destroy(0.3f);
        }
    }
}
