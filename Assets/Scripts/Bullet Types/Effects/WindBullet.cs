using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//created by alec

[CreateAssetMenu(menuName = "BulletEffects/WindBullet", fileName = "WindBullet")]
public class WindBullet : BulletEffect
{
    public float KnockBackForce = 10f;
    public float PlayerKnockBackForce = 10f;

    public override void Initialize(){}
    public override void OnEnemyHit(EnemyTakeDamage type, float damage)
    {
        Debug.LogWarning("old code. pls update"); //make a universal variables singleton?
        /*
        Vector3 normal = type.transform.position - PlayerController.Instance.transform.position;
        if (type.GetComponent<Rigidbody>() != null)
        {
            type.GetComponent<Rigidbody>().AddForce(normal.normalized * KnockBackForce, ForceMode.Impulse);
        }
        PlayerController.Instance.type.GetComponent<Rigidbody>().AddForce(-normal.normalized * PlayerKnockBackForce, ForceMode.Impulse);
        */
    }

    public override void OnHitOther(Vector3 point, float damage)
    {
        
    }
}
