using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class BackgroundManager : MonoBehaviour
    {
        [SerializeField] public AudioSource audioSource;
        [SerializeField] public AudioSource secondaryAudio;
        [SerializeField] private float lenghtOfAudioSwitch;

        private AudioClip current;
        private AudioClip previous;
        private int stageIndex;
        private StageStats[] stageStats;

        [HideInInspector] public bool audioSourcePlayingCurrent = true;
        [HideInInspector] public bool needToPlayAudio = true;
        [HideInInspector] public float volumeSliderAdjustment = 1;

        private void Start()
        {
            stageIndex = StageManager.stageIndex;
            stageStats = StageManager._stages;
            current = stageStats[stageIndex].BackgroundAudio;
            previous = current;
            audioSource.clip = current;
            PublicEvents.OnStageTransition.AddListener(Transition);
            PublicEvents.StartSound.AddListener(StartSound);

            if (audioSource.isPlaying)
            {
                needToPlayAudio = false;
            }

            if (needToPlayAudio)
            {
                audioSource.volume = audioSource.volume * volumeSliderAdjustment;
                audioSource.Play();
            }
        }

        private void StartSound()
        {
            print("HI");
            audioSource.volume = audioSource.volume * volumeSliderAdjustment;
            audioSource.Play();
        }
        private void Transition()
        {
            ++stageIndex;
            previous = current;
            current = stageStats[stageIndex].BackgroundAudio;

            if (current == previous)
            {
                return;
            }
            if (current == null)
            {
                return;
            }

            if (audioSourcePlayingCurrent)
                AudioSourceCurrentTrue();
            else
                AudioSourceCurrentFalse();
        }

        private void AudioSourceCurrentTrue()
        {
            secondaryAudio.clip = current;
            secondaryAudio.volume = 0;
            secondaryAudio.Play();

            StartCoroutine(LerpFunctionDown(0, lenghtOfAudioSwitch, audioSource)); //lenght
            StartCoroutine(LerpFunctionUp(stageStats[stageIndex].BackgroundVolume * volumeSliderAdjustment, lenghtOfAudioSwitch, secondaryAudio));
            audioSourcePlayingCurrent = false;
        }

        private void AudioSourceCurrentFalse()
        {
            audioSource.clip = current;
            audioSource.volume = 0;
            audioSource.Play();

            StartCoroutine(LerpFunctionDown(0, lenghtOfAudioSwitch, secondaryAudio));
            StartCoroutine(LerpFunctionUp(stageStats[stageIndex].BackgroundVolume, lenghtOfAudioSwitch, audioSource));
            audioSourcePlayingCurrent = true;
        }

        IEnumerator LerpFunctionDown(float endVolume, float duration, AudioSource downVol)
        {
            float time = 0;
            float startValue = downVol.volume;
            while (time < duration)
            {
                downVol.volume = Mathf.Lerp(startValue, endVolume, time / duration);
                time += Time.deltaTime;
                yield return null;
            }
            downVol.volume = endVolume;
            //downVol.Stop();
        }

        IEnumerator LerpFunctionUp(float endVolume, float duration, AudioSource upVol)
        {
            float time = 0;
            float startValue = upVol.volume;
            while (time < duration)
            {
                upVol.volume = Mathf.Lerp(startValue, endVolume, time / duration);
                time += Time.deltaTime;
                yield return null;
            }
            upVol.volume = endVolume;
        }

    }
}