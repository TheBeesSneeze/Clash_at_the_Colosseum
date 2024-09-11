using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "BouncingBullet", menuName = "BulletEffects/Bouncing")]
    public class BouncingBullet : BulletEffect
    {

        public override void Initialize() { }
        public override void OnEnemyHit(EnemyTakeDamage type, float damage)
        {

        }

        public override void OnHitOther(Vector3 point, float damage)
        {

        }
    }
}
