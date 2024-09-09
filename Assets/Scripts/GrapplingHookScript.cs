using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHookScript : MonoBehaviour
{
    public float grapplingDistance;
    public Transform shotOrgin;
    public float grapplingShotSpeed;
    public float grapplingPullSpeed;
    bool isHooked;
    public void Start()
    {
        InputEvents.Instance.GrapplingStarted.AddListener(startGrapple);
    }
    public void startGrappling()
    {
        Ray
    }
}
