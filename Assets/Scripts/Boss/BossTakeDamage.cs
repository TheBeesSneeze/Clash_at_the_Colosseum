/*******************************************************************************
* File Name :         BossTakeDamage
* Author(s) :         Clare Grady, Sky
* Creation Date :     9/27/2024
*
* Brief Description : 
* Has the enemy TakeDamage function 
* Has the enemy Die function
 *****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTakeDamage : EnemyTakeDamage
{
    private BossStats bstats;
    [HideInInspector]public GameObject bossBar;

    private BossHealthBar bossHealthBar;
    protected override void Start()
    {
        bstats = GetComponent<BossStats>();
        currentHealth = bstats.BossHealth;
        bossHealthBar = bossBar.GetComponent<BossHealthBar>();
        bossHealthBar.SetMaxHealth(currentHealth);
    }
    public override void TakeDamage(float damage)
    {
        if (!BossController.Invincible)
        {
            base.TakeDamage(damage);
            float temp = currentHealth - damage;
            if (bossHealthBar != null)
            {
                bossHealthBar.SetHealth(temp);
            }
            if (currentHealth < 0)
            {
                Die();
            }
            
        }
           
    }


    public override void Die()
    {
  
        PublicEvents.HydraDeath.Invoke();
        Destroy(gameObject);
    }

 
}
