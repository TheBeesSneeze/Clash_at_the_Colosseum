using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.ParticleSystemModule;
using UnityEngine.ParticleSystemJobs;

public class ParticleManager : MonoBehaviour
{
    public void Start()
    {
        
    }

    public IEnumerator PlayParticleGroups(ParticleThing particles)
    {
        foreach (ParticleSystem particle in particles.particles)
        {
            if (particle != null) 
                particle.Play();
        }
        
        yield return new WaitForSeconds(particles.PlayDuration);

        foreach (ParticleSystem particle in particles.particles)
        {
            if (particle == null) 
                particle.Stop();
        }

    }
}

[System.Serializable]
public class ParticleThing
{
    public float PlayDuration = 1;
    public List<ParticleSystem> particles;
}
