using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLeftUI : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text text;
    [SerializeField] private GunController gunController;
    // Start is called before the first frame update
    void Start()
    {
        PublicEvents.OnPlayerShoot.AddListener(BulletUI);

        if (gunController == null) 
            gunController = GameObject.FindObjectOfType<GunController>();
    }

    private void BulletUI()
    {
        if (text == null)
            return;

        text.text = gunController.GetShotsLeft().ToString() + "/" + gunController.GetShotsTillCoolDown().ToString();
    }
}
