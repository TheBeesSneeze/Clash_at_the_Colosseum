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
        if (gunController.secondsSinceLastShoot > 1 / gunController.shootingMode.ShotsPerSecond)
        {
            group.alpha = 0;
        }
        if (gunController.secondsSinceLastShoot < 1 / gunController.shootingMode.ShotsPerSecond)
        {
            group.alpha = 1;
            BarRefill();
        }
    }
    public void BarRefill()
    {
        reloadBar.value = Mathf.Lerp(0, 1, gunController.secondsSinceLastShoot / (1/gunController.shootingMode.ShotsPerSecond));


    }
}
