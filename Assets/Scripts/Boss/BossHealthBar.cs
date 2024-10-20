using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(CanvasGroup))]
public class BossHealthBar : MonoBehaviour
{
    public Slider slider;
    private BossTakeDamage bossTakeDamage;
    private CanvasGroup group;

    private void Start()
    {
        group = GetComponent<CanvasGroup>();    
        group.alpha = 0;

        PublicEvents.OnBossStart.AddListener(EnableBar);
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
        group.alpha = 1;
        bossTakeDamage = GameObject.FindObjectOfType<BossTakeDamage>();
        bossTakeDamage.bossHealthBar = this;
        SetMaxHealth(bossTakeDamage.GetComponent<BossStats>().BossHealth);
    }

    private void DisableBar()
    {
        group.alpha = 0;
    }
}

