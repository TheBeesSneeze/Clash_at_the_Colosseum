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

        //the radius of a sphere around where the bullet makes contact with a surface
        //if there are enemies in this range it should try and deflect a bullet in that direction
        [SerializeField] private float deflectionAimRange = 100;

        //number of times it can bounce off the wall before it gets destroyed
        [SerializeField] private int numOfBounces = 1;

        public override void Initialize() {
            //isnt this suposed to be like a start function? what does this even do?
            //what im trying to to is make it so that the bullet does not get destroyed when it hits a surface
            DestroyBulletOnSurfaceContact = false;
        }
        public override void OnEnemyHit(EnemyTakeDamage type, float damage){
            // pretty sure this shouldn't do anything special for this bullet effect
        }
        public override void OnHitOther(Vector3 point, float damage)
        {
            //figures out how many bounces the bullet has left
            if (numOfBounces <= 0) {
                DestroyBulletOnSurfaceContact = true;
            } else {
                numOfBounces--;
            }

            //makes a list of colliders of all the enemies in the deflectionAimRange
            Collider[] enemyColliders = Physics.OverlapSphere(point, deflectionAimRange, enemyLayer);

            if (enemyColliders.Length <= 0){
                //means that tere were no enemies within the deflectionAimRange so
                //it will deflect in a random or predetermined direction instead of towards an enemy
            }
            else {
                //turns the bullet so that its new forward direction is facing the nearest enemy
                //enemyColliders[0].transform.position should be the location of the nearest enemy
                Vector3.RotateTowards(_parentBullet.transform.position, enemyColliders[0].transform.position, Mathf.Infinity, 0f);
            }
        }
    }
}
