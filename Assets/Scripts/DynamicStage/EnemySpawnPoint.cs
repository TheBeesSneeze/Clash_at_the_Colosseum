///
/// Nothing burger script. its important tho.
/// Enemies get spawned in enemy spawn
///

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    public EnemySpawn enemyToSpawn;

    private void Start()
    {
        PublicEvents.OnStageTransitionFinish.AddListener(BeforeSpawn);
    }

    private void BeforeSpawn()
    {
        //sky - you know what to do 
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, enemyToSpawn.ToString(), true);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, transform.lossyScale.x);
    }
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 100,~6))
        {
            Gizmos.DrawLine(transform.position, hit.point);
        }
        else
            Gizmos.DrawRay(transform.position, Vector3.down * 100);
    }
}


public enum EnemySpawn { None, GroundedMelee, FlyingRanged, GroundedRanged, Boss }