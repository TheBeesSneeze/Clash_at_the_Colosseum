///
/// Unfinished oops
/// - Toby
///


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VenomBullet : BulletEffect
{
    public GameObject VenomPoolGameObject;
    public override void OnShoot(Bullet bullet) { }
    public override void OnEnemyHit(EnemyTakeDamage type, float damage, Bullet bullet)
    {

    }
    public override void OnHitOther(RaycastHit hit, float damage, Bullet bullet)
    {
        Instantiate(VenomPoolGameObject);
    }

    public override void OnDestroyBullet(Bullet bullet, float damage){ }
}
