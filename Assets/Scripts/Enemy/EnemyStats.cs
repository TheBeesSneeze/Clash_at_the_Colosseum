/*******************************************************************************
* File Name :         EnemyStats
* Author(s) :         Clare Grady
* Creation Date :     8/28/2024
*
* Brief Description : 
* ALL of the Enemy Stats NO IMPLENTATION 
 *****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [Header("Declare ALL enemy stats here")]
    [SerializeField] public float EnemyHealth;
    [SerializeField] public float EnemyDamage;
    [SerializeField] public float EnemySpeed;
    
    [SerializeField] public KindOfEnemy EnemyType;

    public enum KindOfEnemy
    {
        BasicEnemy
    }
}
