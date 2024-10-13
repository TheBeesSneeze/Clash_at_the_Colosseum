using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthSystem : MonoBehaviour
{
    public Slider healCharge;
    [SerializeField] private float maxChargeNeeded;
    private GameObject playerObject; 
    private PlayerBehaviour playerBehaviour;
    private PlayerStats playerStats;
    private bool isHealing;
    private float healPerSecond;
    private float timeElapsed;

    // Start is called before the first frame update
    void Start()
    {
        InputEvents.Instance.HealStarted.AddListener(heal);
        healCharge.value = 0;
        healCharge.maxValue = maxChargeNeeded;
        playerObject = FindObjectOfType<PlayerBehaviour>().gameObject;
        playerBehaviour = playerObject.GetComponent<PlayerBehaviour>();
        playerStats = playerObject.GetComponent<PlayerStats>();

        healPerSecond = (playerStats.DefaultHealth) / (playerStats.secondsTillFull);
        timeElapsed = 0;
    }

    private void Update()
    {
        if (isHealing)
        {
            timeElapsed += Time.deltaTime;
            if(timeElapsed <= playerStats.secondsTillFull)
            {
                float newHealth = Mathf.Max(CalculateHealth(), playerBehaviour.CurrentHealth);
                playerBehaviour.RegenHealth(Mathf.Min(newHealth, playerStats.DefaultHealth));
                /*make a float that holds current health
                 * get default - current to know how much needs healed
                 * calculate what percent that is of default 
                 * calculate healPerSecond
                 * Function to heal correct amount*/
            }
            else 
            {
                playerBehaviour.RegenHealth(playerStats.DefaultHealth);
                isHealing = false;
            }
        }
    }

    private float CalculateHealth()
    {
        float newHealth = timeElapsed * healPerSecond;
        return newHealth;
    }    

    public void addCharge(float charge)
    {
        healCharge.value += charge;
    }

    public void heal()
    {
        if (healCharge.value >= maxChargeNeeded)
        {
            print("healing");
            isHealing = true;
            healCharge.value = 0;
            timeElapsed = 0;
        }
        else
        {
            print("bar not full");
        }
        return;
    }
}
