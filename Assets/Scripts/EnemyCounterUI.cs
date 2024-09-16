using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCounterUI : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text text;

    private void Update()
    {
        EnemyCounter();
    }

    public void EnemyCounter()
    {
        text.text = "Enemies Left: " + EnemySpawner.ReturnEnemyCount().ToString();
    }
}