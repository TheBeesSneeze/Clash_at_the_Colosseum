using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//created by alec

[CreateAssetMenu(menuName = "BulletEffects/WindBullet", fileName = "WindBullet")]
public class WindBullet : BulletEffect
{
    public float KnockBackForce = 10f;
    public float PlayerKnockBackForce = 10f;
    public override void OnShoot(Bullet bullet)
    {
        _playerGunController.GetComponent<Rigidbody>().AddForce(-bullet.transform.forward * PlayerKnockBackForce, ForceMode.Impulse);
    }
    public override void OnEnemyHit(EnemyTakeDamage enemy, float damage, Bullet bullet)
    {
        Vector3 knockbackDirection = enemy.transform.position - GameObject.FindObjectOfType<PlayerController>().gameObject.transform.position;
        if (enemy.GetComponent<Rigidbody>() != null)
        {
            enemy.GetComponent<Rigidbody>().AddForce(knockbackDirection.normalized * KnockBackForce, ForceMode.Impulse);
            enemy.ApplyFallDamage();
        }
    }
    public override void OnHitOther(RaycastHit hit, float damage, Bullet bullet) {}

    public override void OnDestroyBullet(Bullet bullet, float damage) {}
}
