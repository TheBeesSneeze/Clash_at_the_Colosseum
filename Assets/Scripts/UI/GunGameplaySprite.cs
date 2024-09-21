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
        LoadSprite(gunController.shootingMode.GameplaySprite);
    }

    private void OnGunShoot()
    {
        LoadSprite(gunController.shootingMode.GameplayShootSprite);
        time = 0;
    }

    private void LateUpdate()
    {
        time += Time.deltaTime;
        if(time < gunController.shootingMode.ShootSpriteSeconds)
        {
            LoadSprite(gunController.shootingMode.GameplayShootSprite);
            return;
        }
        if(gunController.secondsSinceLastShoot < gunController.shootingMode.ShotsPerSecond)
        {
            LoadSprite(gunController.shootingMode.GameplayCantShootSprite);
            return;
        }
        LoadSprite(gunController.shootingMode.GameplaySprite);
    }

    private void LoadSprite(Sprite sprite)
    {
        if (sprite == null) return;

        gunImage.sprite = sprite;
    }
}
