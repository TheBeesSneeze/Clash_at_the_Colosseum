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

public class EnemyTakeDamage : MonoBehaviour
{
    private EnemyStats stats;
    [HideInInspector] public float currentHealth;
    [SerializeField] private Color damageColor;
   
    [Header("Velocity Damage")]
    [Tooltip("how fast you need to be falling in order to start taking fall damage")]
    [SerializeField] private float VelocityDamageSpeed = 0;
    [Tooltip("how much base damage the Velocity damage does")]
    [SerializeField] private float VelocityDamage = 1;
    [Tooltip("how long after it has been hit by the wind bullet will it try to apply velocity damage")]
    [SerializeField] private float velocityDamageCoolDown = 1;
    private bool doVelocityDamage = false;

    [SerializeField] private float damageColorTime;
    private float damagetime;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private EnemyAnimator enemyAnimator;
    private HealthSystem healthSystem;

    public bool IsDead { get { return currentHealth < 0; } }


    private bool isStillAlive;
    protected virtual void Start()
    {
        stats = GetComponent<EnemyStats>();
        currentHealth = stats.EnemyHealth;
        isStillAlive = true;
        enemyAnimator = GetComponent<EnemyAnimator>();
        healthSystem = FindObjectOfType<HealthSystem>();

    }
    private void Update()
    {
        damagetime -= Time.deltaTime;
        if (damagetime <= 0)
        {
            spriteRenderer.color = Color.white;
        }

        if (transform.position.y < -500)
            stats.OnEnemyDeath();
    }

    public virtual void TakeDamage(float damage){
        if (IsDead)
            return; // bro stop hes already dead

        currentHealth -= damage;
        damagetime = damageColorTime;
        spriteRenderer.color = damageColor;

        PublicEvents.OnEnemyDamage.Invoke();

        if (currentHealth < 0 && isStillAlive) 
        {
            stats.OnEnemyDeath();
            return;
        }

        if (enemyAnimator != null)
            enemyAnimator.OnTakeDamage(currentHealth);
    }
    public virtual void ApplyVelocityDamage() {
        doVelocityDamage = true;
        StartCoroutine(doVelocityDamageCoolDown(velocityDamageCoolDown));
    }
    IEnumerator doVelocityDamageCoolDown(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        doVelocityDamage = false;
    }
    public virtual void Die()
    {
        Debug.Log("die");
        Destroy(gameObject);
        EnemySpawner.OnEnemyDeath();
        isStillAlive = false;
        healthSystem.addCharge(stats.healCharge);

        PublicEvents.OnAnyEnemyDeath.Invoke(stats);

        if(stats.bulletType == EnemyType.Melee){PublicEvents.MinoutarDeath.Invoke();}
        else if(stats.bulletType == EnemyType.Cyclops) { PublicEvents.CyclopsDeath.Invoke(); }
        else if(stats.bulletType == EnemyType.Harpy) { PublicEvents.HarpyDeath.Invoke(); }
    }
    private void OnCollisionEnter(Collision collision)
    {
        float speed = gameObject.GetComponent<Rigidbody>().velocity.magnitude;
        if (speed >= VelocityDamageSpeed && doVelocityDamage)
        {
            print("Velocity Damage Dealt: " + VelocityDamage * (speed - VelocityDamage));
            TakeDamage(VelocityDamage * (speed - VelocityDamage));
        }
    }
}
