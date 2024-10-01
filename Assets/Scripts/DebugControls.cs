using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugControls : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            UpgradeSelectUI ui = GameObject.FindObjectOfType<UpgradeSelectUI>();
            ui.OpenMenu();
        }
    }
}
