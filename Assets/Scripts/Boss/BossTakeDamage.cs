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
    protected override void Start()
    {
        bstats = GetComponent<BossStats>();
        currentHealth = bstats.BossHealth;
    }
    public override void TakeDamage(float damage)
    {
        if (!BossController.Invincible)
            base.TakeDamage(damage);
    }


    public override void Die()
    {
        Debug.Log("boss is dead yahoo");
        Destroy(gameObject);
    }
}