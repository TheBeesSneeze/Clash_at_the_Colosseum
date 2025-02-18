///
/// Toby
/// (omg im on the airplane rn)
///

using BulletEffects;
using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

namespace UI
{
    public class GunGameplaySprite : MonoBehaviour
    {
        [SerializeField] private Image gunImage;
        [SerializeField] private Image bouncingImage;
        [SerializeField] private Image lightningImage;
        [SerializeField] private Image explosionImage;
        [SerializeField] private Image iceImage;
        [SerializeField] private Image windImage;
        [SerializeField] private Image venomImage;

        [SerializeField] private Sprite EmptySprite;
        private GunController gunController;
        private float _time = 0;
        private GunState gunState = GunState.idle;
        private int index;
        private GunAnimation idleAnimation;
        private GunAnimation gunAnimation;

        // Start is called before the first frame update
        void Awake()
        {
            gunController = GameObject.FindObjectOfType<GunController>();

            idleAnimation = gunController.shootingMode.GameplaySprite;
            gunAnimation = gunController.shootingMode.GameplayShootSprite;

            PublicEvents.OnPlayerShoot.AddListener(OnGunShoot);
            PublicEvents.OnUpgradeReceived += OnBulletEffectGet;
        }

        private void OnGunShoot()
        {
            index = 1; //weird ass code btw bro. just going to say it.
            gunState = GunState.shooting;
            _time = 0;

            LoadSprite(gunAnimation.sprites[0]);
        }

        private void Update()
        {
            _time += Time.deltaTime;
            if (gunState == GunState.idle)
            {
                PlayIdleAnimation();
            }
            if (gunState == GunState.shooting)
            {
                PlayShootingAnimation();
            }
        }

        private void OnPlayerShoot()
        {
            gunState = GunState.shooting;
            index = 0;

            return;
        }

        private void PlayIdleAnimation()
        {
            if (_time < idleAnimation.SecondsBetweenFrames)
                return;

            LoadSprite(idleAnimation.sprites[index]);

            _time = 0;
            index = (index + 1) % idleAnimation.sprites.Count;
        }

        private void PlayShootingAnimation()
        {
            if (_time < gunAnimation.SecondsBetweenFrames)
                return;

            LoadSprite(gunAnimation.sprites[index]);
            _time = 0;
            index++;

            if (index >= gunAnimation.sprites.Count)
            {
                gunState = GunState.idle;
                index = 0;
            }

        }

        private void LoadSprite(GunSprite sprite)
        {
            if (sprite == null) return;

            SetGunSprite(sprite);

            LoadIndividualSprite(lightningImage, sprite.lightningSprite);
            LoadIndividualSprite(iceImage, sprite.iceSprite);
            LoadIndividualSprite(windImage, sprite.windSprite);
            LoadIndividualSprite(bouncingImage, sprite.bouncingSprite);
            LoadIndividualSprite(venomImage, sprite.venomSprite);

            if(gunController.GetShotsLeft() > 1)
                LoadIndividualSprite(explosionImage, sprite.bombSprite);
            else
                LoadIndividualSprite(explosionImage, EmptySprite);

        }

        private void OnBulletEffectGet(BulletEffect effect)
        {
            //not super huge on the way im doing this. if u have any better ideas then im open to em

            System.Type type = effect.GetType();
            Debug.Log(type.ToString());

            if (type == typeof(ElectricityBullet)) { if (lightningImage != null) lightningImage.enabled = true; return; }
            if (type == typeof(ExplosionBullet)) { if (explosionImage != null) explosionImage.enabled = true; return; }
            if (type == typeof(SlowBullet)) { if (iceImage != null) iceImage.enabled = true; return; }
            if (type == typeof(WindBullet)) { if (windImage != null) windImage.enabled = true; return; }
            if (type == typeof(VenomBullet)) { if (venomImage != null) venomImage.enabled = true; return; }
            if (type == typeof(BouncingBullet)) { if (bouncingImage != null) bouncingImage.enabled = true; return; }

            LoadSprite(gunAnimation.sprites[index]);
        }

        private void SetGunSprite(GunSprite sprite)
        {
            //what in the magic numbers???
            if (gunController.GetShotsLeft() <= 1)
            {
                LoadIndividualSprite(gunImage, sprite.NoneSnakes);
                return;
            }
            float p = gunController.GetShotsLeftPercent();
            if (gunController.GetShotsFired() <= 2 || p >= 0.666f)
            {
                LoadIndividualSprite(gunImage, sprite.AllSnakes);
                return;
            }
            if (p <= 0.333f)
            {
                LoadIndividualSprite(gunImage, sprite.OneSnakes);
                return;
            }

            LoadIndividualSprite(gunImage, sprite.TwoSnakes);
        }

        private void LoadIndividualSprite(Image image, Sprite sprite, bool emptySprite = false)
        {
            if (image == null) return;
            if (sprite == null) return;
            if (!image.enabled) return;

            if (emptySprite)
            {
                image.sprite = EmptySprite;
                return;
            }

            if (sprite == null || image.sprite == sprite) return;

            image.sprite = sprite;
        }

        public void Refresh()
        {
            foreach (BulletEffect be in gunController.bulletEffects)
            {
                if (be != null)
                    OnBulletEffectGet(be);
            }
        }
    }
    public enum GunState
    {
        idle, shooting
    }
}

