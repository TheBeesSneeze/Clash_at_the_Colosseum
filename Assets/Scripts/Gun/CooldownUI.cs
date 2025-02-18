/*******************************************************************************
* File Name :         CooldownUI
* Author(s) :         Clare Grady
* Creation Date :     10/18/2024
*
* Brief Description : 
* Cooldown UI code for the gun reload. Turns on and off as needed
 *****************************************************************************/
using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CooldownUI : MonoBehaviour
    {
        [SerializeField] private Slider cooldown;
        private float reloadTime;
        private float currentTime;
        private bool isReloading;
        private GunController gunController;

        // Start is called before the first frame update
        void Start()
        {
            currentTime = 0;
            GunController gunController = FindObjectOfType<GunController>();
            reloadTime = gunController.overheatCoolDown;
            cooldown.maxValue = reloadTime;
            cooldown.value = 0;
            gameObject.SetActive(false);

            if (!gunController.shootingMode.canInfiniteFire)
            {
                PublicEvents.Reloading.AddListener(Enable);
            }
        }

        // Update is called once per frame
        void Update()
        {
            currentTime -= Time.deltaTime;
            if (isReloading)
                SetCooldown();
        }

        private void SetCooldown()
        {
            if (currentTime <= 0f)
            {
                isReloading = false;
                gameObject.SetActive(false);
                return;
            }
            cooldown.value = currentTime;

        }
        private void Enable()
        {
            isReloading = true;
            currentTime = reloadTime;
            gameObject.SetActive(true);
        }
    }
}


