/*******************************************************************************
* File Name :         ElectricityBullet.cs
* Author(s) :         Toby Schamberger, Clare Grady
* Creation Date :     3/30/2024
*
* Brief Description : zaps nearby enemies
* 
* TODO visual
 *****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "ElectricityBullet", menuName = "BulletEffects/Electricity")]
    public class ElectricityBullet : BulletEffect
    {
        public float ElectrocutionRange;
        public int MaxEnemiesToZap;
        [SerializeField] LineRenderer line;

        public override void Initialize()
        {
            throw new System.NotImplementedException();
        }

        public override void OnEnemyHit(EnemyTakeDamage type, float damage)
        {
            EnemyTakeDamage[] closeEnemies = GetEnemiesInRange(type);

            if (closeEnemies.Length == 0) return;

            damage = damage * DamageMultiplier;

            Vector3 originEnemy = type.transform.position;

            for(int i=0; i<closeEnemies.Length; ++i)
            {
                Electrocute(closeEnemies[i], damage, originEnemy);
            }
        }

        public override void OnHitOther(Vector3 point, float damage) { }

        private void Electrocute(EnemyTakeDamage enemy, float damage, Vector3 origin)
        {
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Visualize(origin, enemy.transform.position, enemy);
            }
            
        }

        private void Visualize(Vector3 origin, Vector3 target, EnemyTakeDamage enemy)
        {
            //Debug.DrawLine(origin, target, Color.yellow, 2.5f);
            LineControl lineControl = line.GetComponent<LineControl>();
            lineControl.Spawn(origin, target, enemy);
        }

        /// <summary>
        /// does NOT return enemy that got hit
        /// </summary>
        /// <param name="enemy"></param>
        /// <returns></returns>
        private EnemyTakeDamage[] GetEnemiesInRange(EnemyTakeDamage enemy)
        {
            //weird logic im sorry
            //i wanted to challenge myself to not use a list but i really should have tbh
            //wouldve been easier to read
            //sorry if this breaks and u gotta do shit to it later

            Collider[] hitColliders = Physics.OverlapSphere(enemy.transform.position, 3, LayerMask.GetMask("Enemy"));
            
            int l = Mathf.Max(0, hitColliders.Length);
            int length = Mathf.Min(l, MaxEnemiesToZap);
            
            EnemyTakeDamage[] enemies = new EnemyTakeDamage[length];

            for (int i = 0; i < length; i++)
            {
                Debug.Log(hitColliders[i].gameObject.GetComponent<EnemyTakeDamage>());
                if (hitColliders[i].gameObject.GetComponent<EnemyTakeDamage>() != null)
                {
                    EnemyTakeDamage e = hitColliders[i].gameObject.GetComponent<EnemyTakeDamage>();
                    enemies[i] = e;
                }
            }

            return enemies;
        }
    }
}
