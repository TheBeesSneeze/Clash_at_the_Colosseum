using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathFinding;
using TMPro;
using NaughtyAttributes;
using Utilities;

[RequireComponent(typeof(EnemyStats))]
public class GroundedEnemyMovement : MonoBehaviour
{
    private float _turningSpeed = 1;
    private float _stoppingSpeed = 1;
    float _stopDistanceToPlayer = 1;
    bool _needsToSeePlayer = false; //TODO. DOESNT WORK
    private float _sightDistance = 10;
    private EnemyStats _enemyStats;

    private Transform _player;
    public bool debug = true;
    private Rigidbody rb;
    private SphereCollider _collider; //im so sorry it needs to be a sphere

    private Path path;
    private int _playerLayer = 6;
    private LayerMask groundlm;
    [ReadOnly][SerializeField] private Cell currentCell;


    private void Start()
    {
        _player = GameObject.FindObjectOfType<PlayerBehaviour>().transform;
        _playerLayer = LayerMask.NameToLayer("Player");
        groundlm = LayerMask.NameToLayer("Default");
        rb = GetComponent<Rigidbody>();
        _collider = GetComponent<SphereCollider>();
        _enemyStats = GetComponent<EnemyStats>();

        if (_enemyStats == null) return;
        _turningSpeed = _enemyStats.TurningSpeed;
        _stoppingSpeed = _enemyStats.StopSpeed;
        _stopDistanceToPlayer = _enemyStats.StopDistanceToPlayer;
        _sightDistance = _enemyStats.SightDistance;
    }

    private void Update()
    {
        Cell c = UpdateCurrentCell();

        if(c == null)
            return;

        path = GameManager.pathManager.GetPathToPlayer(currentCell);

        if (path == null ) return;
        if (path.nextPath == null) return;

        bool navigating = NavigateToPlayer();
        if (!navigating)
        {
            Debug.LogWarning("enemy stopping");
            float y = rb.velocity.y;
            rb.velocity = Vector3.Lerp(rb.velocity, new Vector3(0, y, 0), Time.deltaTime*_stoppingSpeed);
        }
        RotateColliderTowardsDirection(rb.velocity);

    }

    private void RotateColliderTowardsDirection(Vector3 direction)
    {
        if (direction.magnitude < 0.01f)
            return;

        Quaternion targetRoation = Quaternion.LookRotation(direction);
        rb.transform.rotation = Quaternion.Lerp(transform.rotation, targetRoation, Time.deltaTime * _turningSpeed);
    }

    /// <summary>
    /// returns true if it can navigate to player
    /// </summary>
    /// <returns></returns>
    private bool NavigateToPlayer()
    {
        if (_needsToSeePlayer && !HasClearViewToPoint(_player.transform.position, _playerLayer))
            return false;

        if (Vector3.Distance(transform.position, _player.transform.position) < _stopDistanceToPlayer)
            return false;

        float distance = Vector3.Distance(new Vector3(path.position.x, 0, path.position.z), new Vector3(transform.position.x, 0, transform.position.z));
        //float avgPathSize = (path.cell.transform.lossyScale.x + path.cell.transform.lossyScale.z) / 2;

        if (distance < 0.1f)
            path = path.nextPath;

        path = path.nextPath;

        Vector3 targetPosition = getTargetPosition();
        Vector3 direction = targetPosition - transform.position;
        Debug.DrawLine(transform.position, targetPosition, Color.blue);
        direction = direction.normalized * _enemyStats.MoveSpeed;
        //rb.velocity = Vector3.Lerp( rb.velocity, direction, _enemyStats.MoveSpeed * Time.deltaTime);
        rb.velocity = Vector3.Lerp( rb.velocity, direction, 0.5f );

        return true;
    }

    private Vector3 getTargetPosition()
    {
        if (path == null)
            return _player.position;

        /*
        if (path.nextPath != null)
        {
            Vector3 avgPosition = (path.nextPath.position + path.position) / 2;
            if (HasClearViewToPoint(avgPosition, _emptyCellLayer))
            {
                //DebugUtilities.DrawBox(path.position, path.cell.transform.lossyScale/1.9f, Quaternion.identity, Color.green);
                //DebugUtilities.DrawBox(path.nextPath.position, path.cell.transform.lossyScale/1.9f, Quaternion.identity, Color.green);
                return avgPosition;
            }
        }
        */

        //DebugUtilities.DrawBox(path.position, path.cell.transform.lossyScale/1.9f, Quaternion.identity, Color.green);
        return path.position;
    }

    private bool HasClearViewToPoint(Vector3 point, int layer)
    {
        float distance = Vector3.Distance(point, transform.position);
        return HasClearViewToPoint(point, layer, distance);
    }

    private bool HasClearViewToPoint(Vector3 point, int layer, float distance)
    {
        Vector3 direction = point - transform.position;
        Vector3 radius = _collider.radius * transform.lossyScale / 2.1f;
        if (Physics.BoxCast(transform.position, radius, direction, out RaycastHit hit, Quaternion.identity, distance, layer))
        {
            DebugUtilities.DrawBoxCastBox(transform.position, radius,  direction, Quaternion.identity, distance, Color.magenta);
            return false;
        }
        return true;
    }



    private Cell UpdateCurrentCell()
    {
        //Ray r = new Ray(transform.position - (Vector3.up * transform.lossyScale.y), Vector3.up );
        Ray r = new Ray(transform.position, Vector3.down );
        RaycastHit[] hits = Physics.RaycastAll(r,5 * transform.lossyScale.y, ~groundlm);
        Debug.DrawRay(r.origin, r.direction, Color.white);
        foreach (RaycastHit hit in hits)
        {
            if(hit.transform.TryGetComponent<Cell>( out Cell c))
            {
                return c;
            }
        }
        return null;
    }

    private void OnDrawGizmos()
    {
        if (!debug) return;

        if (currentCell != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(currentCell.transform.position, currentCell.transform.lossyScale*1.1f);
        }    


        if (path == null) return;
        
        if (path.cell == null) return;

        Vector3 lastPoint = path.position;

        Path parth = path; //this is my worst variable name yet
        while (parth != null)
        {
            //Debug.Log(parth.cell.gameObject.name);

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(parth.cell.transform.position, parth.cell.transform.lossyScale);

            Gizmos.color = Color.red;
            Gizmos.DrawLine(lastPoint, parth.cell.transform.position);
            lastPoint = parth.cell.transform.position;

            parth = parth.nextPath;
        }
    }

}
