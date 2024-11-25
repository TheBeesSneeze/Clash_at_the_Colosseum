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
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using Enemy;
using Enemy.Boss;

namespace BulletEffects
{
    [CreateAssetMenu(fileName = "ElectricityBullet", menuName = "BulletEffects/Electricity")]
    public class ElectricityBullet : BulletEffect
    {
        //Clare coded most of this but was originally create by Toby for the GunAdders prototype
        public float ElectrocutionRange;
        public int MaxEnemiesToZap;
        private EnemyStats stats;
        [SerializeField] LineRenderer line;

        public override void OnShoot(Bullet bullet)
        {
            //throw new System.NotImplementedException();
        }

        public override void OnEnemyHit(EnemyTakeDamage type, float damage, Bullet bullet)
        {
            stats = type.GetComponent<EnemyStats>();
            EnemyTakeDamage[] closeEnemies = GetEnemiesInRange(type);

            if (closeEnemies.Length == 0) return;

            damage = damage * DamageMultiplier;

            Vector3 originEnemy = type.transform.position;

            for(int i=0; i<closeEnemies.Length; ++i)
            {
                Electrocute(closeEnemies[i], damage, originEnemy);
            }
        }

        public override void OnHitOther(RaycastHit hit, float damage, Bullet bullet) { }

        private void Electrocute(EnemyTakeDamage enemy, float damage, Vector3 origin)
        {
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Visualize(origin, enemy.transform.position, enemy);

                if (!stats.zappedParticles.isPlaying && stats.zappedParticles != null)
                {
                    stats.zappedParticles.Play();
                }
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
            Collider[] hitColliders = Physics.OverlapSphere(enemy.transform.position, 3, LayerMask.GetMask("Enemy"));
            
            int l = Mathf.Max(0, hitColliders.Length);
            int length = Mathf.Min(l, MaxEnemiesToZap);
            
            EnemyTakeDamage[] enemies = new EnemyTakeDamage[length];

            for (int i = 0; i < length; i++)
            {
                Debug.Log(hitColliders[i].gameObject.GetComponent<EnemyTakeDamage>());
                if (hitColliders[i].gameObject.GetComponent<EnemyTakeDamage>() != null)
                {
                    if (hitColliders[i].gameObject.GetComponent<BossController>() == null)
                    {
                        EnemyTakeDamage e = hitColliders[i].gameObject.GetComponent<EnemyTakeDamage>();
                        enemies[i] = e;
                    }
                    if(stats==null)
                        continue;

                    if(stats.zappedParticles != null)
                        continue;

                    if (!stats.zappedParticles.isPlaying )
                    {
                        stats.zappedParticles.Play();
                    }
                }
            }

            return enemies;
        }

        public override void OnDestroyBullet(Bullet bullet, float damage)
        {
        }
    }
}
