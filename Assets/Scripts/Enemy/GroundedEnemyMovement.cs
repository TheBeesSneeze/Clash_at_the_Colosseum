using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathFinding;

public class GroundedEnemyMovement : MonoBehaviour
{
    [SerializeField] float _speed= 1; 
    public Transform Player;
    public bool debug = true;
    private Rigidbody rb;

    private Path path;
    private Cell currentCell;
    private Transform target;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
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
        Debug.Log(distance);

        if (distance < pathPosition * 0.75f)
            path = path.nextPath;

        if (path != null)
            target = path.cell.transform;
        else
            target = Player;


        Vector3 direction = target.position - transform.position;
        Debug.DrawLine(transform.position, transform.position + direction, Color.blue);
        direction = direction.normalized * _speed;
        rb.velocity = direction;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Cell>())
        {
            currentCell = other.GetComponent<Cell>();
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
