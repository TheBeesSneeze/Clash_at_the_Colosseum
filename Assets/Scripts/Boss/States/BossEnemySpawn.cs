using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
using Managers;
using Enemy;

namespace Enemy.Boss
{
    public class BossEnemySpawn : StateMachineBehaviour
    {
        [SerializeField] private TextAsset[] enemyPlacements;
        private int _currentEnemiesAlive;
        Animator animator;
        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            SpawnEnemies(0);
            this.animator = animator;
            PublicEvents.OnAnyEnemyDeath += OnEnemyDeath;
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            BossController.Invincible = false;
            PublicEvents.OnAnyEnemyDeath -= OnEnemyDeath;
        }

        public void SpawnEnemies(int index)
        {
            Debug.Log("Spawning boss enemies");
            StageLayout sl = StageTransitionManager.GetStageElements(enemyPlacements[index]);
            SpawnPointElement[] enemySpawnPoints = sl.spawnPoints;
            int spawnPointCount = 0;

            for (int i = 0; i < enemySpawnPoints.Length; i++)
            {
                SpawnPointElement s = enemySpawnPoints[i];

                if (BossController.enemySpawner._enemyPrefabs.TryGetValue((EnemySpawn)s.enemyIndex, out GameObject enemyType))
                {

                    Debug.Log("Spawning " + (((EnemySpawn)s.enemyIndex).ToString()) + " from boss");
                    GameObject e = GameObject.Instantiate(enemyType, s.pos, Quaternion.identity);
                    _currentEnemiesAlive++;
                    //e.GetComponent<EnemyTakeDamage>().currentHealth = e.GetComponent<EnemyStats>().EnemyHealth;

                    if (spawnPointCount < EnemySpawner.enemySpawnPoints.Length)
                        StartParticles(EnemySpawner.enemySpawnPoints[spawnPointCount]);
                    spawnPointCount++;
                }
            }
        }

        private void StartParticles(EnemySpawnPoint esp)
        {
            if (esp == null)
                return;
            esp.transform.position = Vector3.zero;
            esp.PlayParticles();
        }

        private void OnEnemyDeath(EnemyStats _)
        {
            //if (!BossController.bossActive)
            //    return;

            _currentEnemiesAlive--;
            Debug.Log(_currentEnemiesAlive);
            if (_currentEnemiesAlive <= 0)
            {
                animator.SetBool("EnemiesDead", true);
            }
        }
    }
}

