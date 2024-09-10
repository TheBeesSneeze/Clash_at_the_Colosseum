///
/// Toby
/// 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyMovement : MonoBehaviour
{
    private float _horizontalSpeed;
    private float _verticalSpeed;
    private float _heightAboveGround;
    private float _stoppingDistanceToPlayer;

    private Transform _player;
    private int _groundMask;
    private Rigidbody rb;
    private EnemyStats enemyStats;


    private void Start()
    {
        _player = GameObject.FindObjectOfType<PlayerBehaviour>().transform;
        _groundMask = LayerMask.NameToLayer("Fill Cell");
        rb = GetComponent<Rigidbody>();
        enemyStats = GetComponent<EnemyStats>();
        _horizontalSpeed = enemyStats.EnemyMovementSpeed;
        _verticalSpeed = enemyStats.VerticalSpeed;
        _heightAboveGround = enemyStats.HeightAboveGround;
        _stoppingDistanceToPlayer = enemyStats.StopDistanceToPlayer;
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
        float targetY;
        //Debug.DrawRay(transform.position, Vector3.down * _heightAboveGround, Color.red);

        //Go Up
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, _heightAboveGround, ~_groundMask))
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
        enemyPos.y = y;
        playerPos.y = y;

        //stay still
        float distance = Vector3.Distance(enemyPos, playerPos);
        if (distance < _stoppingDistanceToPlayer)
            return enemyPos;

        return Vector3.MoveTowards(enemyPos, playerPos, _horizontalSpeed);// * Time.deltaTime);
    }
}
