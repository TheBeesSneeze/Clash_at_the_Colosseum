using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
public class StageUI : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text text;

    private void Update()
    {
        StageUIChanger();
    }
    public void StageUIChanger()
    {
        text.text = "Stage " + (StageManager.GetStageIndex() + 1).ToString();
    }
}
}

