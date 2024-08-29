using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathFinding;
using TMPro;

public class GroundedEnemyMovement : MonoBehaviour
{
    [SerializeField] float _speed= 1; 
    public Transform Player;
    public bool debug = true;
    private Rigidbody rb;

    private Path path;
    private Cell currentCell;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        UpdateCurrentCell();
        if (currentCell == null)
            return;

        path = CellManager.Instance.NavigateToPlayer(currentCell);

        if (path == null) return;

        NavigateToPlayer();
    }

    private void NavigateToPlayer()
    {
        float distance = Vector3.Distance(path.cell.transform.position, transform.position);
        float pathPosition = (path.cell.transform.lossyScale.x + path.cell.transform.lossyScale.y) / 2;
        //Debug.Log(distance);

        if (distance < pathPosition * 0.75f)
            path = path.nextPath;

        Vector3 targetPosition = getTargetPosition();
        Vector3 direction = targetPosition - transform.position;
        Debug.DrawLine(transform.position, transform.position + direction, Color.blue);
        direction = direction.normalized * _speed;
        rb.velocity = direction;
    }

    private Vector3 getTargetPosition()
    {
        if (path == null)
            return Player.position;

        if (path.nextPath != null)
            return (path.nextPath.cell.transform.position + path.cell.transform.position) / 2;

        return (path.cell.transform.position);
    }

    private void UpdateCurrentCell()
    {
        RaycastHit[] hits = Physics.RaycastAll(transform.position, Vector3.up, 1 * transform.lossyScale.y);
        foreach (RaycastHit hit in hits)
        {
            if (hit.transform.GetComponent<Cell>())
            {
                currentCell = hit.transform.GetComponent<Cell>();
                return;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (!debug) return;

        if (path == null)
        {
            return;
        }

        Vector3 lastPoint = path.cell.transform.position;

        Path parth = path;
        while (parth != null)
        {
            Debug.Log(parth.cell.gameObject.name);

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(parth.cell.transform.position, parth.cell.transform.lossyScale);

            Gizmos.color = Color.red;
            Gizmos.DrawLine(lastPoint, parth.cell.transform.position);
            lastPoint = parth.cell.transform.position;

            parth = parth.nextPath;
        }
    }

}
