///
/// Bruh cant believe tyler hayes used tag checks
///

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    [SerializeField] private bool KillPlayer = true;
    [SerializeField] private bool KillEnemy = true;
    private void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<PlayerBehaviour>() != null && KillPlayer)
        {
            other.GetComponent<PlayerBehaviour>().Die();
        }
        if (other.GetComponent<EnemyTakeDamage>() != null && KillEnemy)
        {
            other.GetComponent<EnemyTakeDamage>().Die();
        }
    }
}
