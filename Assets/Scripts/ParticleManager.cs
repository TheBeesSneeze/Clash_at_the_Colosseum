using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.ParticleSystemModule;
using UnityEngine.ParticleSystemJobs;

public class ParticleManager : Singleton<ParticleManager>
{
    public ParticleThing EnemySpawns;
    public void Start()
    {
        PublicEvents.OnStageTransitionFinish.AddListener(EnemySpawns.Play);
        InitalizeParticles(EnemySpawns);
        EnemySpawns.Play(); // it also needs to play at beiginning at game

    }

    public void InitalizeParticles(ParticleThing particles)
    {
        foreach (ParticleSystem particle in particles.particles)
        {
            if (!particles.PlayOnStart)
                particle.Stop();

        }
    }

    //public static IEnumerator PlayParticleGroups(ParticleThing particles)
    //{
        
        
        /*yield return new WaitForSeconds(particles.PlayDuration);

        foreach (ParticleSystem particle in particles.particles)
        {
            if (particle == null) 
                particle.Stop();
        }
        */
    //}
}

[System.Serializable]
public class ParticleThing
{
    //public float PlayDuration = 1;
    public List<ParticleSystem> particles;
    public bool PlayOnStart = false;

    public void Play()
    {
        //ParticleManager.Instance.StartCoroutine(ParticleManager.PlayParticleGroups(this));
        foreach (ParticleSystem particle in particles)
        {
            if (particle != null)
                particle.Play();
        }
    }
}
