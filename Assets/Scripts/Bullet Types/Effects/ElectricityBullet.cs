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

        public override void Initialize()
        {
            throw new System.NotImplementedException();
        }

        public override void OnEnemyHit(EnemyTakeDamage type, float damage)
        {
            Debug.Log("electricity hit");
            EnemyTakeDamage[] closeEnemies = GetEnemiesInRange(type);

            if (closeEnemies.Length == 0) return;

            damage = damage * DamageMultiplier;

            Vector3 originEnemy = type.transform.position;

            for(int i=0; i<closeEnemies.Length; i++)
            {
                Debug.Log("electrocuting");
                Electrocute(closeEnemies[i], 0, originEnemy);
            }
        }

        public override void OnHitOther(Vector3 point, float damage) { }

        private void Electrocute(EnemyTakeDamage enemy, float damage, Vector3 origin)
        {
            Visualize(origin, enemy.transform.position);
            enemy.TakeDamage(damage);
            Debug.Log("I've been electrocuted");
        }

        private void Visualize(Vector3 origin, Vector3 target)
        {
            Debug.LogWarning("electricity visualization not done yet");
            Debug.DrawLine(origin, target, Color.yellow, 0.5f);
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
            Debug.Log(hitColliders.Length);
            
            int l = Mathf.Max(0, hitColliders.Length - 1);
            int length = Mathf.Min(l, MaxEnemiesToZap);
            
            EnemyTakeDamage[] enemies = new EnemyTakeDamage[length];

            for (int i = 0; i < length; i++)
            {
                EnemyTakeDamage e = hitColliders[i].transform.GetComponent<EnemyTakeDamage>();
            }
            Debug.Log(enemies.Length);

            return enemies;
        }
    }
}
