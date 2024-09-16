using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadBar : MonoBehaviour
{
    [SerializeField] private Slider reloadBar;
    [SerializeField] private CanvasGroup group;
    private bool isVisible;
    private GunController gunController;

    private void Start()
    {
        gunController = GameObject.FindObjectOfType<GunController>();
    }

    private void Update()
    {
        Debug.Log(gunController.secondsSinceLastShoot);
        if (gunController.secondsSinceLastShoot > gunController.shootingMode.ShotsPerSecond)
        {
            group.alpha = 0;
        }
        if (gunController.secondsSinceLastShoot < gunController.shootingMode.ShotsPerSecond)
        {
            group.alpha = 1;
            BarRefill();
        }
    }
    public void BarRefill()
    {
        reloadBar.value = Mathf.Lerp(0, 1, gunController.secondsSinceLastShoot / gunController.shootingMode.ShotsPerSecond);


    }
}
