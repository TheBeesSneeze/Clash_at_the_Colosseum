using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCounterUI : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text text;

    private void Start()
    {
        PublicEvents.OnBossSpawn.AddListener(DisableCounter);
    }

    private void Update()
    {
        EnemyCounter();
    }

    public void EnemyCounter()
    {
        if(text.enabled)
            text.text = "Enemies Left: " + EnemySpawner.ReturnEnemyCount().ToString();
    }

    public void DisableCounter()
    {
        text.enabled = false;
    }
}