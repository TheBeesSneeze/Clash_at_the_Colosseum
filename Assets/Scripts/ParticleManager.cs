using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.ParticleSystemModule;
using UnityEngine.ParticleSystemJobs;

public class ParticleManager : Singleton<ParticleManager>
{
    public ParticleGroup EnemySpawns;
    public ParticleGroup PlayerHeal;
    public void Start()
    {
        //PublicEvents.OnStageTransitionFinish.AddListener(EnemySpawns.Play);
        //InitalizeParticles(EnemySpawns);
        //EnemySpawns.Play(); // it also needs to play at beiginning at game

        PublicEvents.OnPlayerHeal.AddListener(PlayerHeal.Play);
        InitalizeParticles(PlayerHeal);

    }

    public void InitalizeParticles(ParticleGroup particles)
    {
        if(particles.PlayOnStart)
        {
            particles.Play();
            return;
        }

        foreach (ParticleSystem particle in particles.particles)
        {
            if (!particles.PlayOnStart && particle != null)
                particle.Stop();
        }
    }
}

[System.Serializable]
public class ParticleGroup
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
            else
                Debug.LogWarning("No Particles set");
        }
    }
}
