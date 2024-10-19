///
/// Delete this script later
///

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackupEnemyMovement : MonoBehaviour
{
    private LayerMask betterLayerMask;
    private Transform player;
    private Rigidbody rb;
    private EnemyStats stats;

    private Transform hitpos;
    // Start is called before the first frame update
    void Start()
    {
        betterLayerMask = LayerMask.GetMask(new string[] { "Default", "Fill Cell", "Player" });
        //betterLayerMask |= (1 << LayerMask.GetMask("Default"));
        //betterLayerMask |= (1 << LayerMask.GetMask("Fill Cell"));
        //betterLayerMask |= (1 << LayerMask.GetMask("Player"));

        player = GameObject.FindObjectOfType<PlayerBehaviour>().transform;
        stats = GetComponent<EnemyStats>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!canSeePlayer()) return;

        Vector3 direction = player.transform.position - transform.position;
        direction.y = 0;
        direction = direction.normalized * stats.MoveSpeed;
        direction.y = rb.velocity.y;

        rb.velocity = direction;
    }

    bool canSeePlayer()
    {
        RaycastHit hit;
        Vector3 direction = player.transform.position - transform.position;
        //float distance = direction.magnitude;
        if(Physics.Raycast(transform.position, direction, out hit, 100, betterLayerMask))
        {
            hitpos = hit.transform;
            if(hit.transform.GetComponent<PlayerBehaviour>() != null)
            {
                Debug.DrawRay(transform.position, direction,Color.green);
                return true;
            }
        }
        Debug.DrawRay(transform.position, direction, Color.red);
        return false;
    }

    private void OnDrawGizmos()
    {
        return;

        /*if (hitpos == null)
            return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(hitpos.transform.position, hitpos.transform.lossyScale*1.2f);*/
    }
}
