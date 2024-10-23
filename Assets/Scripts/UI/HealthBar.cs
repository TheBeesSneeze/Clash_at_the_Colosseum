/*******************************************************************************
* File Name :         HealthBar
* Author(s) :         Clare Grady
* Creation Date :     9/27/2024
*
* Brief Description : 
* Health bar slider UI code
 *****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class HealthBar : MonoBehaviour
{

    [SerializeField] private Slider slider;
    private float value;

    private void Update()
    {
        slider.value = Mathf.Lerp(slider.value, value, 4*Time.deltaTime);
    }

    public void SetHealth(float health)
    {
        value = health;
    }

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        value = health;
    }
}
