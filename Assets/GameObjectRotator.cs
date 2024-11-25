using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectRotator : MonoBehaviour
{
    //Sets the object to a random rotation
    void Start()
    {
        gameObject.transform.rotation = Random.rotation;
    }
}
