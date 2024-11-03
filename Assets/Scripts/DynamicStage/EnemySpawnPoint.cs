///
/// (not anymore) Nothing burger script. its important tho. 
/// Enemies get spawned in enemy spawn
///

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    public EnemySpawn enemyToSpawn;
    private ParticleSystem particles;

    private void Start()
    {
        PublicEvents.OnStageTransitionFinish.AddListener(BeforeSpawn);
        particles = gameObject.GetComponent<ParticleSystem>();
    }
    
    /// <summary>
    /// starts playing particles before enemies spawn in
    /// </summary>
    private void BeforeSpawn()
    {
        //sky - you know what to do 
        //yeah man i got it

        if (enemyToSpawn != EnemySpawn.None)
        {
            PlayParticles();
        }
    }

    public void PlayParticles()
    {
        if (!particles.isPlaying)
        {
            particles.Play(false);
        }
    }

#if UNITY_EDITOR
    public void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, enemyToSpawn.ToString(), true);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, transform.lossyScale.x);

        //this is stuff toby tried to use
        if(TryGetComponent<ParticleSystem>(out ParticleSystem ps))
        {
            ps.playOnAwake = (enemyToSpawn != EnemySpawn.None);
        }
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

#endif
}


public enum EnemySpawn { None, GroundedMelee, FlyingRanged, GroundedRanged, Boss }