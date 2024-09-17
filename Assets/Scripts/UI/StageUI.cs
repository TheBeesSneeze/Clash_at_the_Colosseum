using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageUI : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text text;

    private void Update()
    {
        StageUIChanger();
    }
    public void StageUIChanger()
    {
        text.text = "Stage " + StageManager.ReturnStageIndex().ToString();
    }
}
