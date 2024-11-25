using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using Enemy;
using Player;

namespace Enemy.Boss
{

    public class DamageOnCollision : MonoBehaviour
    {
        [SerializeField] private float dps = 1;
        [SerializeField] private bool damagePlayer = true;
        [SerializeField] private bool damageEnemies = true;
        [SerializeField] private bool ApplyDamageOverTime = true;
        private List<Transform> collisions = new List<Transform>();
        //private float cooldown;

        [EnableIf("ApplyDamageOverTime")]
        [SerializeField] private float SecondsBetweenDamage = 0.1f;
        private float timeOfLastAttack;

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
            if (ApplyDamageOverTime)
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

            if (!ApplyDamageOverTime) return;

            if (collisions.Count <= 0)
                return;

            if (timeOfLastAttack + SecondsBetweenDamage > Time.time)
                return;

            // go time
            timeOfLastAttack = Time.time;
            foreach (Transform t in collisions)
            {
                AttemptAttack(t);
            }
        }

        private void AttemptAttack(Transform col)
        {
            if (col == null)
                return; //cant believe this is a problem


            if (damagePlayer && col.TryGetComponent(out PlayerBehaviour behaviour))
            {
                behaviour.TakeDamage(dps * Time.deltaTime / SecondsBetweenDamage);
            }

            if (damageEnemies && col.TryGetComponent(out EnemyTakeDamage enemy))
            {
                enemy.TakeDamage(dps * Time.deltaTime / SecondsBetweenDamage);
            }

        }
    }
}