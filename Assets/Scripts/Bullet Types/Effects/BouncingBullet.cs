/*******************************************************************************
* File Name :         BouncingBullet.cs
* Author(s) :         Tyler
* Creation Date :     10/1
*
* Brief Description : bullets bounce off walls to the closest enemy if the enemy 
*                     is in dflection range
 *****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "BouncingBullet", menuName = "BulletEffects/Bouncing")]
    public class BouncingBullet : BulletEffect
    {
        [SerializeField] private LayerMask enemyLayer;
        [SerializeField] private float deflectionAimRange = 100;
        [SerializeField] private int numOfBounces = 1;

        public override void Initialize() {
            //idk what this function is suposed to do
        }
        public override void OnEnemyHit(EnemyTakeDamage type, float damage){
            // pretty sure this shouldn't do anything special
        }
        public override void OnHitOther(Vector3 point, float damage)
        {
            if (numOfBounces <= 0) {
                DestroyBulletOnSurfaceContact = true;
                Debug.Log(DestroyBulletOnSurfaceContact);
                numOfBounces--;
            } else {
                DestroyBulletOnSurfaceContact = false;
            }
            Collider[] enemyColliders = Physics.OverlapSphere(point, deflectionAimRange, enemyLayer);
            if (enemyColliders.Length <= 0){
                //mans that tere were no enemies within the deflectionAimRange so
                //it will deflect in a random or predetermined direction
            }
            else {
                Vector3.RotateTowards(_parentBullet.transform.position, enemyColliders[0].transform.position, Mathf.Infinity, 0f);
                DestroyBulletOnSurfaceContact = true;
                Debug.Log("called");
            }
        }
    }
}
