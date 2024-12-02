using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.Boss
{
    public class BossStats : MonoBehaviour
    {

        [Header("Combat Variables")]
        [SerializeField][Min(0)] public float BossHealth;
        //[SerializeField] [Min(0)] public float BossDamage;

        [Header("Movement Variables")]
        [SerializeField][Min(0)] public float bossAttackRate = 1;

        [Header("Particles")]
        [SerializeField] private ParticleSystem frostParticles;

        //[Header("Slow Variables")]
        // [SerializeField] public Color SlowedColor;
        [HideInInspector]
        public float BossAttackRate
        {
            get
            {
                if (slowedDownCountdown <= 0)
                    return bossAttackRate;
                else return bossAttackRate/ slowedAttackRate;
            }
        }

        private float slowedDownCountdown;
        private float slowedRate;
        private float slowedAttackRate;


        #region affectors


        //updating countdown timer
        private void Update()
        {
            //ColorChange();
            BossController.animator.SetFloat("Slow", BossAttackRate);
            slowedDownCountdown -= Time.deltaTime;

            //sorry guys this is such a long if statement
            if (slowedDownCountdown > 0 && !frostParticles.isPlaying && frostParticles != null)
            {
                frostParticles.Play(false);
                frostParticles.enableEmission = true;
            }
            else if (slowedDownCountdown <= 0 && frostParticles.isPlaying)
            {
                frostParticles.Stop(false);
                frostParticles.enableEmission = false;
            }
        }

        /// <summary>
        /// slows the enemy and starts the countdown timer
        /// </summary>
        /// <param name="slowedAmount"></param>
        /// <param name="slowedTime"></param>
        public void SlowBoss(float slowedAmount, float slowedTime, float attackSpeedMultiplier)
        {
            slowedRate = slowedAmount;
            slowedDownCountdown = slowedTime;
            slowedAttackRate=attackSpeedMultiplier;
        }

        /* public void ColorChange()
         {
             if (slowedDownCountdown <= 0)
                 BossController.BossSR.color = Color.white;
             else
                 BossController.BossSR.color = SlowedColor;
         }
        */
        #endregion

    }
}
