using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLeftUI : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text text;
    [SerializeField] private GunController gunController;

    private bool canInfiniteFire;
    // Start is called before the first frame update
    void Start()
    {
        PublicEvents.OnPlayerShoot.AddListener(BulletUI);
        PublicEvents.OnPlayerReload.AddListener(BulletUI);

        if (gunController == null) 
            gunController = GameObject.FindObjectOfType<GunController>();

        canInfiniteFire = gunController.shootingMode.canInfiniteFire;

        BulletUI();
    }

    private void BulletUI()
    {
        if (text == null)
            return;
        if (canInfiniteFire)
        {
            text.text = "infinty / infinty".ToString();
            return;
        }

        text.text = gunController.GetShotsLeft().ToString() + "/" + gunController.GetShotsTillCoolDown().ToString();
    }
}
