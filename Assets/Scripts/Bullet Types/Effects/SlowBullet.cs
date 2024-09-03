/*******************************************************************************
* File Name :         DefaultNamespace.cs
* Author(s) :         Alec
* Creation Date :     
*
* Brief Description : 
 *****************************************************************************/

using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "SlowBullet", menuName = "BulletEffects/SlowBullet")]
    public class SlowBullet : BulletEffect
    {
        [field:SerializeField] public float SlowTime { get; private set; }= 2f;
        public override void Initialize()
        {
        }
        public override void OnEnemyHit(EnemyTakeDamage type, float damgae)
        {
            Debug.LogWarning("old code. pls update");
            //slow down the enemy some how
            //type.ApplySlow(SlowTime);
        }

        public override void OnHitOther(Vector3 point, float damage)
        {
            
        }
    }
}