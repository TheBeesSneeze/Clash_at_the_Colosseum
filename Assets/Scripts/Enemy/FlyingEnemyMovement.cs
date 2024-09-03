///
/// Toby
/// 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyMovement : MonoBehaviour
{
    [SerializeField][Min(0)] private float _horizontalSpeed = 3;
    [SerializeField][Min(0)] private float _verticalSpeed = 1;
    [SerializeField][Min(0)] private float _heightAboveGround = 10;
    [SerializeField][Min(0)] private float _stoppingDistanceToPlayer = 1;

    private Transform _player;
    private int _groundMask;
    private Rigidbody rb;


    private void Start()
    {
        _player = GameObject.FindObjectOfType<PlayerBehaviour>().transform;
        _groundMask = LayerMask.NameToLayer("Fill Cell");
        rb = GetComponent<Rigidbody>();
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
        Debug.DrawRay(transform.position, Vector3.down * _heightAboveGround, Color.red);

        //Go Up
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, _heightAboveGround, _groundMask))
        { 
            targetY = hit.point.y + _heightAboveGround;
        }
        // Go Down
        else
        { 
            targetY = currentY - _verticalSpeed;
        }

        float difference = Mathf.Abs(currentY - targetY);
        float t = _verticalSpeed * Time.deltaTime / difference;
        float y = Mathf.Lerp(currentY, targetY, t);
        return y;
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

        return Vector3.MoveTowards(enemyPos, playerPos, _horizontalSpeed * Time.deltaTime);
    }
}
