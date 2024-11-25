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
using UI;

namespace Enemy.Boss
{
public class BossTakeDamage : EnemyTakeDamage
{
    private BossStats bstats;
    //[HideInInspector]public GameObject bossBar;

    public BossHealthBar bossHealthBar;
    protected override void Start()
    {
        bstats = GetComponent<BossStats>();
        currentHealth = bstats.BossHealth;
    }
    public override void TakeDamage(float damage)
    {
        if (!BossController.Invincible)
        {
            base.TakeDamage(damage);
            if (bossHealthBar != null)
            {
                bossHealthBar.SetHealth(currentHealth);
            }
            if (currentHealth <= 0)
            {
                Die();
            }

        }

    }


    public override void Die()
    {

        PublicEvents.HydraDeath.Invoke();
        Debug.Log("i died");
        //Destroy(gameObject);
    }


}
}

