/*******************************************************************************
* File Name :         EnemyTakeDamage
* Author(s) :         Clare Grady, Sky
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
    [SerializeField] private Color damageColor;
    [SerializeField] private float damageColorTime;
    private float damagetime;
    [SerializeField] private SpriteRenderer spriteRenderer;



    private bool isStillAlive;
    private void Start()
    {
        stats = GetComponent<EnemyStats>();
        currentHealth = stats.EnemyHealth;
        isStillAlive = true;
    }
    private void Update()
    {
        damagetime -= Time.deltaTime;
        if (damagetime <= 0)
        {
            spriteRenderer.color = Color.white;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        damagetime = damageColorTime;
        spriteRenderer.color = damageColor;

        if (currentHealth < damage && isStillAlive) 
        {
            Die();
        }
        else
        {
            PublicEvents.OnEnemyDamage.Invoke();
        }
    }

    private void Die()
    {
        EnemySpawner.OnEnemyDeath();
        isStillAlive = false;

        PublicEvents.OnEnemyDeath.Invoke();
        Destroy(gameObject);

    }

}
