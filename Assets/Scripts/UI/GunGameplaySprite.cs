using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

public class GunGameplaySprite : MonoBehaviour
{
    [SerializeField] private Image gunImage;
    private GunController gunController;
    private float _time=0;
    private GunState gunState = GunState.idle;
    private int index;
    private GunAnimation idleAnimation;
    private GunAnimation gunAnimation;
    
    // Start is called before the first frame update
    void Start()
    {
        gunController = GameObject.FindObjectOfType<GunController>();

        idleAnimation = gunController.shootingMode.GameplaySprite;
        gunAnimation = gunController.shootingMode.GameplayShootSprite;

        PublicEvents.OnPlayerShoot.AddListener(OnGunShoot);
    }

    private void OnGunShoot()
    {
        index = 0;
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
        index = (index+1)%idleAnimation.sprites.Count;
    }

    private void PlayShootingAnimation()
    {
        if (_time < gunAnimation.SecondsBetweenFrames)
            return;

        LoadSprite(gunAnimation.sprites[0]);
        _time = 0;
        index++;

        if(index >= gunAnimation.sprites.Count)
        {
            gunState = GunState.idle;
            index = 0;
        }

        Debug.Log("shooting animation");
    }

    private void LoadSprite(GunSprite sprite)
    {
        if (sprite == null) return;

        gunImage.sprite = sprite.baseSprite;
    }
}
public enum GunState
{
    idle, shooting
}
