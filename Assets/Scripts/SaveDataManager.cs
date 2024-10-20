using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataManager : Singleton<SaveDataManager>
{
    GunController gc;
    UpgradeSelectUI ui;
    GunGameplaySprite ggs;
    // Start is called before the first frame update
    void Start()
    {
        ///Find object of type sucks so bad i will fix this later
       
        gc = GameObject.FindObjectOfType<GunController>();
        gc.bulletEffects.Clear();
        foreach(BulletEffect be in SaveData.gotBulletEffects)
        {
            gc.AddBulletEffect(be,false);
        }

        ui = GameObject.FindObjectOfType<UpgradeSelectUI>();

        ui.OverridePool(SaveData.bulletEffectPool);

        if(SaveData.CurrentStageIndex >= 1)
        {
            gc.transform.position = gc.transform.position + (Vector3.up * 10);
        }

        ggs = GameObject.FindObjectOfType<GunGameplaySprite>();
        ggs.Refresh();
    }

    private void OnDisable()
    {
        Debug.Log("Restarting");
        SaveData.CurrentStageIndex = StageManager.stageIndex;

        if (SaveData.CurrentStageIndex >= StageManager._stages.Length)
            SaveData.CurrentStageIndex -= 1;

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
