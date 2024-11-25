///
/// Toby
/// 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace Enemy
{
public class FlyingEnemyMovement : MonoBehaviour
{
    private float _horizontalSpeed;
    private float _verticalSpeed;
    private float _heightAboveGround;
    private float _stoppingDistanceToPlayer;
    private float _flyingOffset;
    private float r;
    private Transform _player;
    private int _groundMask;
    private Rigidbody rb;
    private EnemyStats enemyStats;

    private Vector3 playerOffset;

    private void Awake()
    {
        r = Random.Range(-1, 1);
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError(gameObject.name + " does not have a rigidbody");
            Destroy(this);
        }
    }
    private void Start()
    {
        _player = GameObject.FindObjectOfType<PlayerBehaviour>().transform;
        _groundMask = LayerMask.GetMask(new string[] { "Fill Cell", "Default" });

        enemyStats = GetComponent<EnemyStats>();
        _horizontalSpeed = enemyStats.MoveSpeed;
        _verticalSpeed = enemyStats.VerticalSpeed;
        _heightAboveGround = enemyStats.HeightAboveGround;
        _stoppingDistanceToPlayer = enemyStats.StopDistanceToPlayer;
        _flyingOffset = enemyStats.MovementOffset;
        playerOffset = new Vector3(Random.Range(-_flyingOffset, _flyingOffset), Random.Range(-_flyingOffset, _flyingOffset), Random.Range(-_flyingOffset, _flyingOffset));
    }

    private void Update()
    {
        float y = GetYPosition();
        Vector3 targetPosition = GetHorizontalPosition(y);
        Vector3 direction = targetPosition - transform.position;

        rb.velocity = direction;
    }

    private float GetYPosition()
    {
        float currentY = transform.position.y;
        //Debug.DrawRay(transform.position, Vector3.down * _heightAboveGround, Color.red);

        //Go Up
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, _heightAboveGround, _groundMask))
        {
            Debug.DrawLine(transform.position, hit.point, Color.red);
            return hit.point.y + _heightAboveGround;
        }
        return currentY - _verticalSpeed;
    }

    private Vector3 GetHorizontalPosition(float y)
    {
        Vector3 enemyPos = transform.position;
        Vector3 playerPos = _player.position;
        playerPos += playerOffset;
        enemyPos.y = y;
        playerPos.y = y;

        playerPos.x += r;
        playerPos.z += r;

        //stay still
        float distance = Vector3.Distance(enemyPos, playerPos);
        if (distance < _stoppingDistanceToPlayer)
            return enemyPos;

        return Vector3.MoveTowards(enemyPos, playerPos, _horizontalSpeed);// * Time.deltaTime);
    }
}
}


