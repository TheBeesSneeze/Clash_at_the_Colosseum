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

public class BossTakeDamage : MonoBehaviour
{
    private BossStats stats;
    private float currentHealth;
    [SerializeField] private Color damageColor;
    [SerializeField] private float damageColorTime;
    private float damagetime;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private bool isStillAlive;

    private void Start()
    {
        stats = GetComponent<BossStats>();
        currentHealth = stats.BossHealth;
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
            //sounds?????
           // PublicEvents.OnBossDamage.Invoke();
        }
    }

    public void Die()
    {
        //animator reference for death animation
        Destroy(gameObject);

    }

}
