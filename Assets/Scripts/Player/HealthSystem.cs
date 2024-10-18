using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthSystem : MonoBehaviour
{
    [SerializeField] private Slider healCharge;
    [SerializeField] private float maxChargeNeeded;
    [SerializeField] private Image controlPrompt;
    private PlayerBehaviour playerBehaviour;
    private PlayerStats playerStats;
    private bool isHealing;
    private float healPerSecond;
    private float timeElapsed;
    private float value = 0f;

    // Start is called before the first frame update
    void Start()
    {
        InputEvents.Instance.HealStarted.AddListener(heal);
        healCharge.value = 0;
        healCharge.maxValue = maxChargeNeeded;
        playerBehaviour = FindObjectOfType<PlayerBehaviour>();
        playerStats = playerBehaviour.GetComponent<PlayerStats>();

        healPerSecond = (playerStats.DefaultHealth) / (playerStats.secondsTillFull);
        timeElapsed = 0;
        controlPrompt.enabled = false;
    }

    private void Update()
    {
        //little animation :)
        healCharge.value = Mathf.Lerp(healCharge.value, value, 4*Time.deltaTime);

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
        value += charge;
        value = Mathf.Min(value, maxChargeNeeded); // cap at maxChargeNeeded

        if (value == maxChargeNeeded)
            controlPrompt.enabled = true;
    }

    public void heal()
    {
        if (value >= maxChargeNeeded)
        {
            print("healing");
            isHealing = true;
            healCharge.value = 0;
            timeElapsed = 0;
            PublicEvents.OnPlayerHeal.Invoke();
            controlPrompt.enabled = false;
        }
        else
        {
            print("bar not full");
        }
        return;
    }
}
