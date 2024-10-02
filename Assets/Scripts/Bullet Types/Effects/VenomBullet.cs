using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VenomBullet : BulletEffect
{
    public GameObject VenomPoolGameObject;
    public override void Initialize() { }
    public override void OnEnemyHit(EnemyTakeDamage type, float damage)
    {

    }
    public override void OnHitOther(Vector3 point, float damage)
    {
        Instantiate(VenomPoolGameObject);
    }
}
