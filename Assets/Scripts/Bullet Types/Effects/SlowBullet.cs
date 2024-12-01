/*******************************************************************************
* File Name :         SlowBullet.cs
* Author(s) :         Sky
* Creation Date :     
*
* Brief Description : 
 *****************************************************************************/

using UnityEngine;
using Enemy;
using Enemy.Boss;

namespace BulletEffects
{
    [CreateAssetMenu(fileName = "SlowBullet", menuName = "BulletEffects/SlowBullet")]
    public class SlowBullet : BulletEffect
    {
        [SerializeField] private float SlowBulletSpeed;
        [SerializeField] private float EnemySlowedTime;
        [SerializeField] private float EnemyAttackSpeedMultiplier;
        public override void OnShoot(Bullet bullet) {}
        public override void OnEnemyHit(EnemyTakeDamage type, float damage, Bullet bullet)
        {
            if(type.TryGetComponent<EnemyStats>(out EnemyStats es))
            {
                es.SlowEnemy(SlowBulletSpeed, EnemySlowedTime, EnemyAttackSpeedMultiplier);
            }
            if (type.TryGetComponent<BossStats>(out BossStats bs))
            {
                bs.SlowBoss(SlowBulletSpeed, EnemySlowedTime, EnemyAttackSpeedMultiplier);
            }
        }
        public override void OnHitOther(RaycastHit hit, float damage, Bullet bullet){}

        public override void OnDestroyBullet(Bullet bullet, float damage){ }
    }
}