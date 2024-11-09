using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class BulletLeftUI : MonoBehaviour
{
    [SerializeField] private GunController gunController;
    [SerializeField] private Slider slider;

    private bool canInfiniteFire;
    // Start is called before the first frame update
    void Start()
    {
        if (gunController == null) 
            gunController = GameObject.FindObjectOfType<GunController>();

        canInfiniteFire = gunController.shootingMode.canInfiniteFire;
        
        if(!canInfiniteFire)
        {
            PublicEvents.OnPlayerShoot.AddListener(BulletUI);
            PublicEvents.OnPlayerReload.AddListener(BulletUI);
        }
        BulletUI();
        slider.maxValue = gunController.shootingMode.ClipSize;
        slider.value = gunController.shootingMode.ClipSize; 
    }

    private void SetValue(int value)
    {
        slider.value = value;
    }

    private void BulletUI()
    {
        SetValue(gunController.GetShotsLeft());
    }
}
