///
/// Spawns venom puddles and such
/// its finished now
/// - Toby
///


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Enemy;

namespace BulletEffects
{
    [CreateAssetMenu(menuName = "BulletEffects/VenomBullet", fileName = "VenomBullet")]
    public class VenomBullet : BulletEffect
    {
        [SerializeField] private GameObject VenomPoolGameObject;
        [SerializeField] private float distanceToGround = 3;
        [SerializeField] LayerMask groundmask;

        public override void OnShoot(Bullet bullet) { }
        public override void OnEnemyHit(EnemyTakeDamage type, float damage, Bullet bullet)
        {
            TrySpawnVenom(type.transform.position);
        }
        public override void OnHitOther(RaycastHit hit, float damage, Bullet bullet)
        {
            TrySpawnVenomRotated(hit, bullet);
        }

        public override void OnDestroyBullet(Bullet bullet, float damage) { }


        private void TrySpawnVenom(Vector3 hitPoint)
        {
            if (getFloorPoint(hitPoint, out Vector3 position))
            {
                GameObject venom = Instantiate(VenomPoolGameObject, position, Quaternion.identity); //TODO object pooling?
                venom.transform.LookAt(venom.transform.position + Vector3.down);
            }

        }

        private void TrySpawnVenomRotated(RaycastHit hit, Bullet bullet)
        {
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            Vector3 direction = Vector3.Reflect(Vector3.up, hit.normal);
            GameObject venom = Instantiate(VenomPoolGameObject, hit.point, Quaternion.identity);
            venom.transform.SetParent(hit.transform);
            venom.transform.LookAt(hit.point + hit.normal);
            venom.transform.position = hit.point;
        }

        private bool getFloorPoint(Vector3 hitPoint, out Vector3 position)
        {
            Debug.DrawRay(hitPoint, Vector3.down * distanceToGround, Color.magenta);


            if (Physics.Raycast(hitPoint + (Vector3.up * 0.01f), Vector3.down, out RaycastHit hit, distanceToGround, groundmask))
            {
                if (Vector3.Dot(Vector3.up, hit.normal) > 0.9f) //if the surface is pretty much flat
                {
                    position = hit.point;
                    position.y += 0.05f;
                    return true;
                }

            }

            position = Vector3.zero;
            Debug.Log("no venom spot");
            return false;
        }
    }
}


