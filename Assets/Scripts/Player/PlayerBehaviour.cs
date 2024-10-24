/*******************************************************************************
 * File Name :         PlayerBehaviour.cs
 * Author(s) :         Toby
 * Creation Date :     3/18/2024
 *
 * Brief Description : The player code that does NOT have to do with input. 
 * Health / collisions / whatever.
 * 
 * projectile collisions are handles in AttackType.cs
 *****************************************************************************/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerStats))]
public class PlayerBehaviour : MonoBehaviour
{
    private PlayerStats stats;
    [SerializeField] private Image redVignette;
    [SerializeField] private HealthBar healthBar;
    [HideInInspector] public float CurrentHealth = 0;
    public bool canTakeDamage = true;
    private float secondsSinceLastTookDamage;
    private LayerMask groundmask;
    
    
    // Start is called before the first frame update
    protected void Start()
    {
        groundmask = LayerMask.GetMask(new string[] { "Fill Cell", "Default" });
        SetStats();

        if (redVignette == null)
        {
            Debug.LogWarning("no damage vignette");
            return;
        }
        if(healthBar != null)
            healthBar.SetMaxHealth(stats.DefaultHealth);
    }

    public void Update()
    {
        /*if (secondsSinceLastTookDamage >= stats.SecondsUntilHealing)
        {
            //RegenHealth();
        }*/
        secondsSinceLastTookDamage += Time.deltaTime;
        
        float t = CurrentHealth / stats.DefaultHealth;
        t = 1 - t;
        if(redVignette == null)
        {
            return;
        }
        redVignette.color = new Color(redVignette.color.r, redVignette.color.g, redVignette.color.b, t * t);

    }

    public void RegenHealth(float healAmount)
    {
        CurrentHealth = healAmount; 
        healthBar.SetHealth(CurrentHealth);
    }

    public void SetStats()
    {
        stats = GetComponent<PlayerStats>();

        CurrentHealth = stats.DefaultHealth;
        healthBar = FindObjectOfType<HealthBar>();
    }
    
    public void TakeDamage(float damage)
    {
        Debug.Log("take damage." + damage);
        if (canTakeDamage) {
            CurrentHealth = CurrentHealth - damage;
        }
        secondsSinceLastTookDamage = 0;

        if(CurrentHealth <= 0)
        {
            Die();
            return;
        }

        healthBar.SetHealth(CurrentHealth);
        PublicEvents.OnPlayerDamage.Invoke();
    }
    public void Die()
    {
        PublicEvents.OnPlayerDeath.Invoke();
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public Vector3 GetGroundPosition()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, Mathf.Infinity, groundmask))
        {
            return hit.point;
        }
        return transform.position;
    }
}
