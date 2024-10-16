using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class AudioManager : MonoBehaviour
{

    private GunController gunController;
    private PlayerBehaviour player;

    public SoundClip GunSound;
    public SoundClip EnemyDamageSound;
    public SoundClip PlayerDamageSound;
    public SoundClip EnemyDeathSound;
    public SoundClip PlayerDeathSound;
    public SoundClip EnemyShootSound;
    public SoundClip MeleeAttackSound;
    public SoundClip GrappleSound;
    public SoundClip DashSound;
    public SoundClip UpgradeReceiveSound;
    public SoundClip StageTransitionSound;
    public SoundClip SnakeCooldownSound;
    public SoundClip HydraIntroSound;
    public SoundClip HydraFireAttackSound;
    public SoundClip HydraDeathSound;
    public SoundClip CyclopsAttackSound;

    public static float masterVolume=1;

    void Start()
    {
        masterVolume = PlayerPrefs.GetFloat("volume", 1);

        PublicEvents.OnPlayerShoot.AddListener(OnGunShoot);
        PublicEvents.OnPlayerDamage.AddListener(PlayerDamage);
        PublicEvents.OnEnemyDamage.AddListener(EnemyDamage);
        PublicEvents.OnEnemyDeath.AddListener(EnemyDeath);
        PublicEvents.OnPlayerDeath.AddListener(PlayerDeath);
        PublicEvents.OnEnemyShoot.AddListener(EnemyShoot);
        PublicEvents.OnMeleeEnemyAttack.AddListener(MeleeEnemyAttack);
        PublicEvents.OnGrapple.AddListener(Grapple);
        PublicEvents.OnDash.AddListener(Dash);
        PublicEvents.OnUpgradeReceived += UpgradeReceived; //bro why are you different
        PublicEvents.OnStageTransition.AddListener(StageTransition);


        gunController = GameObject.FindObjectOfType<GunController>();
        player = GameObject.FindObjectOfType<PlayerBehaviour>();
    }
    //snake, trident, bow sound
    private void OnGunShoot()
    {
        GunSound.PlaySound();
    }

    //enemy damage
    private void EnemyDamage()
    {
        EnemyDamageSound.PlaySound();
    }

    //player damage
    private void PlayerDamage()
    {
        PlayerDamageSound.PlaySound();
    }
    //enemy death
    private void EnemyDeath()
    {
        EnemyDeathSound.PlaySound();

    }

    //player death
    private void PlayerDeath()
    {
        PlayerDeathSound.PlaySound();
    }

    //ranged, tank enemy shooting

    private void EnemyShoot()
    {
        EnemyShootSound.PlaySound();
    }

    //melee enemy attack 
    private void MeleeEnemyAttack()
    {
        MeleeAttackSound.PlaySound();
    }
    
    //grappling hook
    private void Grapple()
    {
        GrappleSound.PlaySound();
    }

    //dash
    private void Dash()
    {
        DashSound.PlaySound();
    }

    //upgrade receive
    private void UpgradeReceived(BulletEffect bulletEffect)
    {
        UpgradeReceiveSound.PlaySound();
    }

    //stage transition
    private void StageTransition()
    {
        StageTransitionSound.PlaySound();
    }

    //cooldown sound ---------------------------------------------
    //boss sound ------------------------------------
}

[System.Serializable]
public class SoundClip
{
    [Range(0f, 1f)]
    public float Volume = 1;
    public AudioClip sound;

    [Button]
    public void PlaySound()
    {
        if (sound != null)
        {
            AudioSource.PlayClipAtPoint(sound, Camera.main.transform.position, Volume * AudioManager.masterVolume);
        }

    }

    //figure this out laters
    public void PlaySound(Vector3 transform)
    {
        if (sound != null)
        {
            AudioSource.PlayClipAtPoint(sound, transform, Volume * AudioManager.masterVolume);
        }
    }
}

