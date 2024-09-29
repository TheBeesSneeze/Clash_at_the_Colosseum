using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnCollision : MonoBehaviour
{
    [SerializeField] private float damage=1;
    [SerializeField] private bool damagePlayer=true;
    [SerializeField] private bool damageEnemies=true;

    public void OnTriggerEnter(Collider other)
    {
        if(damagePlayer && other.TryGetComponent(out PlayerBehaviour behaviour))
        {
            behaviour.TakeDamage(damage);
        }

        if(damageEnemies && other.TryGetComponent(out EnemyTakeDamage enemy))
        {
            enemy.TakeDamage(damage);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        OnTriggerEnter(collision.collider);
    }
}
