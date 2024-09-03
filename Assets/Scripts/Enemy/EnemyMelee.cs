/*******************************************************************************
* File Name :         EnemyMelee
* Author(s) :         Clare Grady
* Creation Date :     9/2/2024
*
* Brief Description : 
* For MELEE enemies only 
* Calculates distance between self and player 
* If player in range attack
 *****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMelee : MonoBehaviour
{
    [SerializeField] GameObject playerObject;

    private EnemyStats stats;
    private PlayerBehaviour player;

    private void Start()
    {
        stats = GetComponent<EnemyStats>();
        player = playerObject.GetComponent<PlayerBehaviour>();
    }
    private void Update()
    {
        DetectPlayer();
    }

    private void DetectPlayer()
    {
        var distance = (transform.position - playerObject.transform.position).normalized;
        distance.y = 0f;
        float distanceFrom = distance.magnitude;

        if(distanceFrom <= stats.EnemyAttackRange)
        {
            player.TakeDamage(stats.EnemyDamage); 
        }
    }

   
}
