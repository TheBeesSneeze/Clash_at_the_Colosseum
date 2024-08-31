/*******************************************************************************
* File Name :         EnemyMovement
* Author(s) :         Clare Grady
* Creation Date :     8/30/2024
*
* Brief Description : 
* Takes in what kind of enemy it is
* Determines movement based on type of enemy
 *****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private EnemyStats enemyBasics;

    [SerializeField] Transform playerLocation; 

    private void Start()
    {
       enemyBasics = GetComponent<EnemyStats>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        switch (enemyBasics.EnemyType)
        {
            case EnemyStats.KindOfEnemy.BasicEnemy:
                MoveBasicEnemy();
                break;
                   
        }
    }

    private void MoveBasicEnemy()
    {

    }
    
}
