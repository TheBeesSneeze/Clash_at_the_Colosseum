/*******************************************************************************
* File Name :         BossHealthBar
* Author(s) :         Clare Grady
* Creation Date :     10/16/2024
*
* Brief Description : 
* Omg its the boss health bar 
 *****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(CanvasGroup))]
public class BossHealthBar : MonoBehaviour  //I don't know why i didn't just derive from health bar
{
    public Slider slider;
    private BossTakeDamage bossTakeDamage;
    private CanvasGroup group;

    private void Start()
    {
        group = GetComponent<CanvasGroup>();    
        group.alpha = 0;

        PublicEvents.OnBossStart.AddListener(EnableBar);
        PublicEvents.OnBossPhaseTwoStart.AddListener(HalfOpacity);
        PublicEvents.OnBossPhaseThreeStart.AddListener(ReturnFullOpacity);
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
    private void HalfOpacity()
    {
        group.alpha = 0.5f;
    }

    private void ReturnFullOpacity()
    {
        group.alpha = 1;
    }

    private void DisableBar()
    {
        group.alpha = 0;
    }
}

