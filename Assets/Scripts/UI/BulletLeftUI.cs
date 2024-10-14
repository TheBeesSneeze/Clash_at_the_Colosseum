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
        
    }

    // Update is called once per frame
    void Update()
    {
        BulletUI();
    }

    private void BulletUI()
    {
        text.text = gunController.GetShotsLeft().ToString() + "/" + gunController.GetShotsTillCoolDown().ToString();
    }
}
