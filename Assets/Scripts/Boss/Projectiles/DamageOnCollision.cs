using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using Unity.VisualScripting;
using UnityEngine;

public class DamageOnCollision : MonoBehaviour
{
    [SerializeField] private float dps=1;
    [SerializeField] private bool damagePlayer=true;
    [SerializeField] private bool damageEnemies=true;
    [SerializeField] private bool ApplyDamageOverTime = true;
    private List<Transform> collisions = new List<Transform>();
    //private float cooldown;



    public void OnTriggerEnter(Collider other)
    {
        if (ApplyDamageOverTime)
        {
            collisions.Add(other.transform);
            return;
        }

        if (damagePlayer && other.TryGetComponent(out PlayerBehaviour behaviour))
        {
            behaviour.TakeDamage(dps);
        }

        if (damageEnemies && other.TryGetComponent(out EnemyTakeDamage enemy))
        {
            enemy.TakeDamage(dps);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(ApplyDamageOverTime)
            collisions.Remove(other.transform);
    }

    public void OnCollisionEnter(Collision collision)
    {
        OnTriggerEnter(collision.collider);
    }
    public void OnCollisionExit(Collision collision)
    {
        OnTriggerExit(collision.collider);
    }

    private void Update()
    {
        //cooldown -= Time.deltaTime;

        if (collisions.Count <= 0)
            return;

        foreach (Transform t in collisions)
        {
            AttemptAttack(t);
        }
    }

    private void AttemptAttack(Transform col)
    {
        if (damagePlayer && col.TryGetComponent(out PlayerBehaviour behaviour))
        {
            behaviour.TakeDamage(dps * Time.deltaTime);
        }

        if (damageEnemies && col.TryGetComponent(out EnemyTakeDamage enemy))
        {
            enemy.TakeDamage(dps*Time.deltaTime);
        }

    }
}
