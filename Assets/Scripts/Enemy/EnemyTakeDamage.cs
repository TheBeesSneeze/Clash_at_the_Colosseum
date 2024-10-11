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
using Utilities;

[RequireComponent(typeof(EnemyStats))]
public class EnemyTakeDamage : MonoBehaviour
{
    private EnemyStats stats;
    [HideInInspector] public float currentHealth;
    [SerializeField] private Color damageColor;
    [SerializeField] private float damageColorTime;
    private float damagetime;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private EnemyAnimator enemyAnimator;


    private bool isStillAlive;
    protected virtual void Start()
    {
        stats = GetComponent<EnemyStats>();
        currentHealth = stats.EnemyHealth;
        isStillAlive = true;
        enemyAnimator = GetComponent<EnemyAnimator>();

    }
    private void Update()
    {
        damagetime -= Time.deltaTime;
        if (damagetime <= 0)
        {
            spriteRenderer.color = Color.white;
        }
    }

    public virtual void TakeDamage(float damage)
    {
        if (enemyAnimator != null)
            enemyAnimator.OnTakeDamage(currentHealth);

        currentHealth -= damage;
        damagetime = damageColorTime;
        spriteRenderer.color = damageColor;

        if (currentHealth < damage && isStillAlive) 
        {
            stats.OnEnemyDeath();
        }
        PublicEvents.OnEnemyDamage.Invoke();
    }

    public virtual void Die()
    {
        Destroy(gameObject);
        EnemySpawner.OnEnemyDeath();
        isStillAlive = false;
        PublicEvents.OnEnemyDeath.Invoke();
    }

}
