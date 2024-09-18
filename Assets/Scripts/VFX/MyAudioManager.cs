using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyAudioManager : MonoBehaviour
{
    //sorry guys im cleaning this up later but right now i feel like puking
    private GunController gunController;
    public SoundClip GunSound;
    public SoundClip EnemyDamageSound;
    public SoundClip PlayerDamageSound;
    public SoundClip EnemyDeathSound;
    private PlayerBehaviour player;

    void Start()
    {        
        PublicEvents.OnPlayerShoot.AddListener(OnGunShoot);
        PublicEvents.OnEnemyDamage.AddListener(EnemyDamage);
        PublicEvents.OnEnemyDeath.AddListener(EnemyDeath);
        PublicEvents.OnPlayerDamage.AddListener(PlayerDamage);


        gunController = GameObject.FindObjectOfType<GunController>();
        player = GameObject.FindObjectOfType<PlayerBehaviour>();
    }
    private void OnGunShoot()
    {
        GunSound.PlaySound();
    }

    private void EnemyDamage()
    {
        EnemyDamageSound.PlaySound();
    }

    private void PlayerDamage()
    {
        PlayerDamageSound.PlaySound();
    }
    private void EnemyDeath()
    {
        EnemyDeathSound.PlaySound();

    }
}

[System.Serializable]
public class SoundClip
{
    [Range(0f, 1f)]
    public float Volume;
    public AudioClip sound;
    public void PlaySound()
    {
        AudioSource.PlayClipAtPoint(sound, Camera.main.transform.position, Volume);

    }
    public void PlaySound(Vector3 transform)
    {
        AudioSource.PlayClipAtPoint(sound, transform, Volume);

    }
}

