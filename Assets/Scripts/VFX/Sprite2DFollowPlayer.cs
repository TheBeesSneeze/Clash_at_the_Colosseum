using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*****************************************************************************
// File Name :         Sprite2DFollowPlayer.cs
// Author :            Toby
//
// Brief Description : Four sprite script to visualize enemies
*****************************************************************************/

public class Sprite2DFollowPlayer : MonoBehaviour
{
    private static Transform player;

    // Use this for initialization
    void Start()
    {
        if(player  == null)
            player = GameObject.FindObjectOfType<PlayerBehaviour>().transform;
    }

    void LateUpdate()
    {
        transform.LookAt(player.position);

    }

}
