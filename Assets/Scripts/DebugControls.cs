using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugControls : MonoBehaviour
{
#if UNITY_EDITOR
    public ShootingMode GodGun;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            EnemyTakeDamage[] enemies = GameObject.FindObjectsOfType<EnemyTakeDamage>(); //idgaf if findobjectsoftype sucks THIS IS DEBUG TOWN

            foreach (EnemyTakeDamage enemy in enemies)
            {
                enemy.TakeDamage(9999999); 
            }

            //UpgradeSelectUI ui = GameObject.FindObjectOfType<UpgradeSelectUI>();
            //ui.OpenMenu();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            HealthSystem hs = GameObject.FindObjectOfType<HealthSystem>();
            hs.addCharge(99999);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            FindObjectOfType<GunController>().LoadShootingMode(GodGun);
        }
    }
#endif
}
