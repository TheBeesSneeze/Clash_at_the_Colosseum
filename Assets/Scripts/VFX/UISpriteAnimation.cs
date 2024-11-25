///
/// Animator was giving me too much trouble man i cant take it anymore
///

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UISpriteAnimation : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private Sprite[] frames;
        [SerializeField] private float animationLengthSeconds = 1;
        [SerializeField] private bool alwaysAnimate = false;

        public float secondsBetweenFrames;
        private float lastFrameChangeTime;
        private CanvasGroup group;
        private int spriteFrameIndex = 0;

        public void SetSprites(Sprite[] sprites)
        {
            if (sprites.Length <= 0)
            {
                Debug.LogError("No sprites!");
                return;
            }

            frames = sprites;
            spriteFrameIndex = 0;
            secondsBetweenFrames = animationLengthSeconds / ((float)frames.Length);
            lastFrameChangeTime = Time.time;
            NextSprite();
        }


        private void Start()
        {
            if (frames.Length <= 0)
            {
                //this is intended behaviour now
                //Debug.LogWarning("Animation not set properly");
                //Destroy(this); 
                return;
            }

            secondsBetweenFrames = animationLengthSeconds / ((float)frames.Length);
            lastFrameChangeTime = Time.time;
            group = GetComponent<CanvasGroup>();
        }


        private void Update()
        {
            //if (group != null)
            //if (group.alpha <= 0 && !alwaysAnimate)
            //return;

            if (frames.Length <= 0) return;


            if (Time.unscaledTime < lastFrameChangeTime + secondsBetweenFrames)
                return;

            //its go time
            NextSprite();
        }
        private void NextSprite()
        {
            lastFrameChangeTime = Time.unscaledTime;
            secondsBetweenFrames = animationLengthSeconds / ((float)frames.Length);
            spriteFrameIndex = (spriteFrameIndex + 1) % frames.Length;
            image.sprite = frames[spriteFrameIndex];
        }


    }
}