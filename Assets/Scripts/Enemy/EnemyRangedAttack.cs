/*******************************************************************************
* File Name :         EnemyRangedAttack
* Author(s) :         Clare Grady
* Creation Date :     9/2/2024
*
* Brief Description : 
* For RANGED Enemies ONLY
* Checks if player is in the attack range 
* If is shoot bullet
 *****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedAttack : MonoBehaviour
{
    private EnemyStats stats;
    private GunController gunController;
    private GameObject playerObject;

    private void Start()
    {
        stats = GetComponent<EnemyStats>();
        gunController = GetComponent<GunController>(); 
        playerObject = stats.playerObject;
    }

    private void FixedUpdate()
    {
        
    }
}
