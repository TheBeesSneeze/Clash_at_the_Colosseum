using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class Tutorialmusic : MonoBehaviour
    {
        [HideInInspector] public float volumeSliderAdjustment;
        [SerializeField] private AudioSource musicSource;

        private float baseVolume = 1;
        // Start is called before the first frame update
        void Start()
        {
            musicSource.playOnAwake = false;
            baseVolume = musicSource.volume;
        }

        public void StartMusic()
        {
            musicSource.volume = baseVolume * volumeSliderAdjustment;
            musicSource.Play();
        }

        public void UpdateVolume()
        {
            musicSource.volume = baseVolume * volumeSliderAdjustment;
        }
    }
}