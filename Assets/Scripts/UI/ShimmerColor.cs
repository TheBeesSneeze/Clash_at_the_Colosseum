using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShimmerColor : MonoBehaviour
{
    [SerializeField] private Selectable target;
    [SerializeField] public Color normalColor1 = Color.white;
    [SerializeField] public Color normalColor2 = Color.white;
    [SerializeField] private float transitionSeconds = 1;

    // Update is called once per frame
    void Update()
    {
        if (!target.interactable) return;

        float t = (Mathf.Cos(Time.unscaledTime * ((Mathf.PI)/2))+1)/2;

        ColorBlock cb = target.colors;
        cb.normalColor = Color.Lerp(normalColor1, normalColor2 , t);
        target.colors = cb;
    }
}
