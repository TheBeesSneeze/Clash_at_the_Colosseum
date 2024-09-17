using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunGameplaySprite : MonoBehaviour
{
    [SerializeField] private Image gunImage;
    private GunController gunController;
    private float time=0;
    // Start is called before the first frame update
    void Start()
    {
        gunController = GameObject.FindObjectOfType<GunController>();

        PublicEvents.OnPlayerShoot.AddListener(OnGunShoot);
    }

    private void OnGunShoot()
    {
        gunImage.sprite = gunController.shootingMode.GameplayShootSprite;
        time = 0;
    }

    private void LateUpdate()
    {
        time += Time.deltaTime;
        if(time < gunController.shootingMode.ShootSpriteSeconds)
        {
            gunImage.sprite = gunController.shootingMode.GameplayShootSprite;
            return;
        }
        if(gunController.secondsSinceLastShoot < gunController.shootingMode.ShotsPerSecond)
        {
            gunImage.sprite = gunController.shootingMode.GameplayCantShootSprite;
            return;
        }
        gunImage.sprite = gunController.shootingMode.GameplaySprite;
    }
}
