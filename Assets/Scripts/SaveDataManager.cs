using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;
using Player;
using BulletEffects;

namespace Managers
{

    public class SaveDataManager : Singleton<SaveDataManager>
    {
        GunController gc;
        UpgradeSelectUI ui;
        GunGameplaySprite ggs;

        /// <summary>
        /// get the save data and load it up baby
        /// </summary>
        void Start()
        {
            ///Find object of type sucks so bad i will fix this later

            gc = GameObject.FindObjectOfType<GunController>();
            //gc.bulletEffects.Clear();
            int temp = SaveData.gotBulletEffects.Count;
            for(int i = 0; i < temp; i++)
            {
                gc.AddBulletEffect(SaveData.gotBulletEffects[i], true);
            }

            /*foreach (BulletEffect be in SaveData.gotBulletEffects)
            {
                gc.AddBulletEffect(be, true);
            }*/


            ui = GameObject.FindObjectOfType<UpgradeSelectUI>();
            if (ui != null)
                ui.OverridePool(SaveData.bulletEffectPool);

            if (SaveData.CurrentStageIndex >= 1)
            {
                // respawn move the player up so they dont clip through the ground
                gc.transform.position = gc.transform.position + (Vector3.up * 25);
            }

            //why
            ggs = GameObject.FindObjectOfType<GunGameplaySprite>();
            if (ggs != null)
                ggs.Refresh();

        }

        private void OnDisable()
        {
            Debug.Log("Restarting");
            SaveData.CurrentStageIndex = StageManager.stageIndex;

            if (SaveData.CurrentStageIndex >= StageManager._stages.Length)
                SaveData.CurrentStageIndex -= 1;


            if (ui != null)
                SaveData.bulletEffectPool = ui.GetPool();
            SaveData.gotBulletEffects = gc.bulletEffects;
        }

        public void OnApplicationQuit()
        {
            SaveData.gotBulletEffects.Clear();
            SaveData.bulletEffectPool.Clear();
            SaveData.CurrentStageIndex = 0;
        }

    }
}

