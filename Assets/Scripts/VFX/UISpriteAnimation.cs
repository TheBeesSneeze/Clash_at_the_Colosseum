///
/// Animator was giving me too much trouble man i cant take it anymore
///

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISpriteAnimation : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private List<Sprite> frames;
    [SerializeField] private float animationLengthSeconds = 1;
    [SerializeField] private bool alwaysAnimate = false;

    public float secondsBetweenFrames;
    private float lastFrameChangeTime;
    private CanvasGroup group;
    public int spriteFrameIndex = 0;

    private void Start()
    {
        if (frames.Count <= 0)
        {
            Debug.LogWarning("Animation not set properly");
            Destroy(this);
        }

        secondsBetweenFrames = animationLengthSeconds / ((float)frames.Count);
        lastFrameChangeTime = Time.time;
        group = GetComponent<CanvasGroup>();
    }


    private void Update()
    {
        //if (group != null)
            //if (group.alpha <= 0 && !alwaysAnimate)
                //return;

        if (Time.unscaledTime < lastFrameChangeTime + secondsBetweenFrames)
            return;

        //its go time
        NextSprite();   
    }
    private void NextSprite()
    {
        lastFrameChangeTime = Time.unscaledTime;
        spriteFrameIndex = (spriteFrameIndex + 1) % frames.Count;
        image.sprite = frames[spriteFrameIndex];
    }
}
