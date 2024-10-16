using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class BossHealthBar : MonoBehaviour
{
    public Slider slider;
    private BossTakeDamage bossTakeDamage;

    private void Start()
    {
        gameObject.SetActive(false);
        PublicEvents.OnBossSpawn.AddListener(EnableBar);
        PublicEvents.HydraDeath.AddListener(DisableBar);
    }

    public void SetHealth(float health)
    {
        slider.value = health;
    }

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    private void EnableBar()
    {
        gameObject.SetActive(true);
        bossTakeDamage = FindObjectOfType<BossTakeDamage>();
        bossTakeDamage.bossBar = this.gameObject;
    }

    private void DisableBar()
    {
        gameObject.SetActive(false);    
    }
}

