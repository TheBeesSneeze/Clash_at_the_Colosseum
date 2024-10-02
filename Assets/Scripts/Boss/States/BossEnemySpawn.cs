using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class BossEnemySpawn : StateMachineBehaviour
{
    [SerializeField] private TextAsset[] enemyPlacements;
    private int _currentEnemiesAlive;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        StageLayout sl = StageTransitionManager.GetStageElements(enemyPlacements[0]);
        SpawnPointElement[] enemySpawnPoints = sl.spawnPoints;

        for (int i = 0; i < enemySpawnPoints.Length; i++)
        {
            SpawnPointElement s = enemySpawnPoints[i];

            if (BossController.enemySpawner._enemyPrefabs.TryGetValue((EnemySpawn)s.enemyIndex, out GameObject enemyType))
            {

                Debug.Log("is this working"); 
                GameObject.Instantiate(enemyType, s.pos, Quaternion.identity);
                _currentEnemiesAlive++;
            }
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
