using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathFinding;
using TMPro;
using NaughtyAttributes;

public class GroundedEnemyMovement : MonoBehaviour
{
    [SerializeField] float _speed= 1; 
    public Transform Player;
    public bool debug = true;
    private Rigidbody rb;

    private Path path;
    private int _emptyCellLayer = 6;
    [ReadOnly][SerializeField] private Cell currentCell;

    private void Start()
    {
        _emptyCellLayer = LayerMask.NameToLayer("Empty Cell");
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        UpdateCurrentCell();
        if (currentCell == null)
            return;

        path = CellManager.Instance.GetPathToPlayer(currentCell);

        if (path == null ) return;
        if (path.nextPath == null) return;
        path = path.nextPath;

        NavigateToPlayer();
    }

    private void NavigateToPlayer()
    {
        float distance = Vector3.Distance(path.cell.transform.position, transform.position);
        float pathPosition = (path.cell.transform.lossyScale.x + path.cell.transform.lossyScale.y) / 2;

        if (distance < pathPosition * 0.15f)
            path = path.nextPath;

        Vector3 targetPosition = getTargetPosition();
        Vector3 direction = targetPosition - transform.position;
        Debug.DrawLine(transform.position, targetPosition, Color.blue);
        direction = direction.normalized * _speed;
        rb.velocity = direction;
        RotateColliderTowardsDirection(direction);
    }

    private Vector3 getTargetPosition()
    {
        if (path == null)
            return Player.position;

        if (path.nextPath != null)
        {
            //Vector3 avgPosition = (path.nextPath.cell.transform.position + path.cell.transform.position) / 2;
            //if(HasClearViewToPoint(avgPosition))
            //    return avgPosition;
        }

        return path.cell.transform.position;
    }

    private bool HasClearViewToPoint(Vector3 point)
    {
        Vector3 direction = point - transform.position;
        float distance = Vector3.Distance(point, transform.position);
        if(Physics.BoxCast(transform.position, transform.lossyScale, direction, out RaycastHit hit, Quaternion.identity,distance, ~_emptyCellLayer))
        {
            Debug.Log("unclear view");
            return false;
        }
        Debug.Log("clear view");
        return true;
    }

    private void RotateColliderTowardsDirection(Vector3 direction)
    {
        transform.rotation.SetLookRotation(direction);
    }

    private void UpdateCurrentCell()
    {
        Ray r = new Ray(transform.position - (Vector3.up * transform.lossyScale.y), Vector3.up );
        RaycastHit[] hits = Physics.RaycastAll(r,2 * transform.lossyScale.y);
        Debug.DrawRay(r.origin, r.direction, Color.white);
        foreach (RaycastHit hit in hits)
        {
            Cell c = hit.transform.GetComponent<Cell>();
            if (c!=null && !c.Solid)
            {
                Debug.Log("cell mode");
                currentCell = c;
                return;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (!debug) return;

        if (currentCell != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(currentCell.transform.position, currentCell.transform.lossyScale*1.1f);
        }    


        if (path == null)
        {
            return;
        }

        Vector3 lastPoint = path.cell.transform.position;

        Path parth = path;
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
