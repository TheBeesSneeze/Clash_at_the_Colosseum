using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField] public AudioSource audioSource;
    [SerializeField] public AudioSource secondaryAudio;
    [SerializeField] private float  lenghtOfAudioSwitch;
    private AudioClip current;
    private AudioClip previous;
    private int stageIndex;
    private StageStats[] stageStats;
    
    [HideInInspector] public bool audioSourcePlayingCurrent = true;

    private void Start()
    {
        stageIndex = StageManager.stageIndex;
        stageStats = StageManager._stages;
        current = stageStats[stageIndex].BackgroundAudio;
        previous = current;
        audioSource.clip = current;
        audioSource.Play();
        PublicEvents.OnStageTransition.AddListener(Transition);
    }

    private void Transition()
    {
        ++stageIndex;
        previous = current;
        current = stageStats[stageIndex].BackgroundAudio;

        if(current == previous)
        {
            return; 
        }
        if(current == null)
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

        StartCoroutine(LerpFunctionDown(0, lenghtOfAudioSwitch, audioSource));
        StartCoroutine(LerpFunctionUp(stageStats[stageIndex].BackgroundVolume, lenghtOfAudioSwitch, secondaryAudio));
    }

    private void AudioSourceCurrentFalse()
    {
        audioSource.clip = current;
        audioSource.volume = 0;
        audioSource.Play();

        StartCoroutine(LerpFunctionDown(0, lenghtOfAudioSwitch, secondaryAudio));
        StartCoroutine(LerpFunctionUp(stageStats[stageIndex].BackgroundVolume, lenghtOfAudioSwitch, audioSource));
    }

    IEnumerator LerpFunctionDown(float endVolume, float duration, AudioSource downVol)
    {
        float time = 0;
        float startValue = downVol.volume;
        while(time < duration)
        {
            downVol.volume = Mathf.Lerp(startValue, endVolume, time/duration);
            time += Time.deltaTime;
            yield return null;
        }
        downVol.volume = endVolume;
        downVol.Stop();
    }

    IEnumerator LerpFunctionUp(float endVolume, float duration, AudioSource upVol)
    {
        float time = 0;
        float startValue = upVol.volume;
        while(time < duration)
        {
            upVol.volume = Mathf.Lerp(startValue,endVolume, time/duration);
            time += Time.deltaTime;
            yield return null;
        }
        upVol.volume = endVolume;
    }

}
