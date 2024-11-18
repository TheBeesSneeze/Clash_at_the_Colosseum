using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using BulletEffects;
using Player;

namespace Managers
{

    public class AudioManager : MonoBehaviour
    {

        private GunController gunController;
        private PlayerBehaviour player;

        public SoundClip GunSound;
        public SoundClip EnemyDamageSound;
        public SoundClip PlayerDamageSound;
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
        public SoundClip CyclopsDeathSound;
        public SoundClip HarpyDeathSound;
        public SoundClip MinoutarDeathSound;
        public SoundClip PageFlipSound;


        public static float masterVolume = 1;

        void Start()
        {
            masterVolume = PlayerPrefs.GetFloat("volume", 1);

            PublicEvents.OnPlayerShoot.AddListener(OnGunShoot);
            PublicEvents.OnPlayerReload.AddListener(Cooldown);
            PublicEvents.OnPlayerDamage.AddListener(PlayerDamage);
            PublicEvents.OnEnemyDamage.AddListener(EnemyDamage);
            PublicEvents.OnPlayerDeath.AddListener(PlayerDeath);
            PublicEvents.OnEnemyShoot.AddListener(EnemyShoot);
            PublicEvents.OnMeleeEnemyAttack.AddListener(MeleeEnemyAttack);
            PublicEvents.OnGrapple.AddListener(Grapple);
            PublicEvents.OnDash.AddListener(Dash);
            PublicEvents.OnUpgradeReceived += UpgradeReceived; //bro why are you different
            PublicEvents.OnStageTransition.AddListener(StageTransition);
            PublicEvents.OnBossSpawn.AddListener(HydraIntro);
            PublicEvents.HydraFireAttack.AddListener(HydraFireAttack);
            PublicEvents.HydraDeath.AddListener(HydraDeath);
            PublicEvents.CyclopsAttack.AddListener(CyclopsAttack);
            PublicEvents.CyclopsDeath.AddListener(CyclopsDeath);
            PublicEvents.HarpyDeath.AddListener(HarpyDeath);
            PublicEvents.MinoutarDeath.AddListener(MinotaurDeath);
            PublicEvents.OnJournalPageFlip.AddListener(PageFlipSound.PlaySound);

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

        //overheat
        private void Cooldown()
        {
            SnakeCooldownSound.PlaySound();
        }

        //hydra spawn
        private void HydraIntro()
        {
            HydraIntroSound.PlaySound();
        }

        //hydra fireball
        private void HydraFireAttack()
        {
            HydraFireAttackSound.PlaySound();
        }

        //hydra dies
        private void HydraDeath()
        {
            HydraDeathSound.PlaySound();
        }

        //cyclops dies
        private void CyclopsDeath()
        {
            CyclopsDeathSound.PlaySound();
        }

        //harpy dies
        private void HarpyDeath()
        {
            HarpyDeathSound.PlaySound();
        }

        //minoutar death
        private void MinotaurDeath()
        {
            MinoutarDeathSound.PlaySound();
        }

        //cyclops attacks 
        private void CyclopsAttack()
        {
            CyclopsAttackSound.PlaySound();
        }

    }

    [System.Serializable]
    public class SoundClip
    {
        [Range(0f, 1f)]
        public float Volume = 1;
        public AudioClip sound;
        public bool RandomizePitch = false;

        [EnableIf("RandomizePitch")]
        [AllowNesting]
        [Tooltip("Default Pitch is 1. Adds a random number, r, such that -n < r < n")]
        public float randomPitchRange = 0.1f;

        [Button]
        public void PlaySound()
        {
            if (sound == null)
                return;


            AudioSource.PlayClipAtPoint(sound, Camera.main.transform.position, Volume * AudioManager.masterVolume);


        }

        //figure this out laters
        public void PlaySound(Vector3 transform)
        {
            if (RandomizePitch)
            {
                // PlayClipAtPoint has no way of setting the pitch
                // It also creates a new gameobject in the scene and destroys it. This code is basically doing the same thing.
                GameObject gobj = new GameObject();
                gobj.transform.position = transform;
                AudioSource audio = gobj.AddComponent<AudioSource>();
                float pitch = 1 + Random.Range(-randomPitchRange, randomPitchRange);
                audio.pitch = pitch;
                audio.PlayOneShot(sound, Volume * AudioManager.masterVolume);
                //GameObject.Destroy(gobj, audio.clip.length / pitch);
                GameObject.Destroy(gobj, audio.clip.length / pitch);
                return;

            }
            if (sound != null)
            {
                AudioSource.PlayClipAtPoint(sound, transform, Volume * AudioManager.masterVolume);
            }
        }
    }


}
