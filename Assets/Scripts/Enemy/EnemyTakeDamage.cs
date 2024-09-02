/*******************************************************************************
* File Name :         EnemyTakeDamage
* Author(s) :         Clare Grady
* Creation Date :     8/31/2024
*
* Brief Description : 
* Has the enemy TakeDamage function 
* Has the enemy Die function
 *****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTakeDamage : MonoBehaviour
{
    private EnemyStats stats;
    private float currentHealth;

    private void Start()
    {
        stats = GetComponent<EnemyStats>();
        currentHealth = stats.EnemyHealth; 
    }

    public void TakeDamage(float damage)
    {
        print("Damage Taken");
        currentHealth -= damage;

        if (currentHealth < damage) 
        {
            Die();        
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

}
