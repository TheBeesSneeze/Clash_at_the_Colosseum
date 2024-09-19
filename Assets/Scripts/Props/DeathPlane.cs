///
/// Bruh cant believe tyler hayes used tag checks
///

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerBehaviour>() != null)
        {
            other.GetComponent<PlayerBehaviour>().Die();
        }

        if (other.GetComponent<EnemyTakeDamage>() != null)
        {
            other.GetComponent<EnemyTakeDamage>().Die();
        }
    }
}
