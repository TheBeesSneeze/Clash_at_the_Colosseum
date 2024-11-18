using mainMenu;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

namespace Enemy
{
    public class OutlineBehaviour : MonoBehaviour
    {

        [Tooltip("The amount of enemies remaining to activate outliner.")]
        [Min(1)]
        public int EnemiesLeftToEnableOutline;
        public void Start()
        {
            PublicEvents.OnAnyEnemyDeath += OnEnemyDeath;
        }

        public void OnEnemyDeath(EnemyStats stats)
        {
            if (EnemySpawner.ReturnEnemyCount() <= EnemiesLeftToEnableOutline)
            {
                Outline outline = gameObject.GetComponentInChildren<Outline>();

                if (outline != null)
                {
                    outline.enabled = true;
                }
            }
        }

        public void OnDestroy()
        {
            PublicEvents.OnAnyEnemyDeath -= OnEnemyDeath;
        }
    }
}


