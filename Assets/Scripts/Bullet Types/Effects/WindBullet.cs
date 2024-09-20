using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//created by alec

[CreateAssetMenu(menuName = "BulletEffects/WindBullet", fileName = "WindBullet")]
public class WindBullet : BulletEffect
{
    public float KnockBackForce = 10f;
    public float PlayerKnockBackForce = 10f;
    public override void Initialize()
    {
        _playerGunController.GetComponent<Rigidbody>().AddForce(-_parentBullet.transform.forward * PlayerKnockBackForce, ForceMode.Impulse);
    }
    public override void OnEnemyHit(EnemyTakeDamage enemy, float damage)
    {
        Vector3 knockbackDirection = enemy.transform.position - GameObject.FindObjectOfType<PlayerController>().gameObject.transform.position;
        if (enemy.GetComponent<Rigidbody>() != null)
        {
            enemy.GetComponent<Rigidbody>().AddForce(knockbackDirection.normalized * KnockBackForce, ForceMode.Impulse);
        }
    }
    public override void OnHitOther(Vector3 point, float damage){}
}
