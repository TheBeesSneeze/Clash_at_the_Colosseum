using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System.Threading.Tasks;
public class DashCameraZoom : MonoBehaviour
{
    [InfoBox("Animation curves are smart. They will always start and end at the default FOV value (as set on the camera component)")]

    [SerializeField]
    private AnimationCurve dashBegin;
    [SerializeField]
    private AnimationCurve dashEnd;

    private float defaultFOV;
    private Camera mainCamera;

    private float begin_animationDuration;
    private float begin_startValue; //values are scaled 
    private float begin_endValue;

    void Start()
    {
        mainCamera = Camera.main;
        defaultFOV = Camera.main.fieldOfView;

        refitAnimationCurve(dashBegin);
        refitAnimationCurve(dashEnd);

        scaleAnimationCurveByFirstValue(dashBegin, defaultFOV);
        scaleAnimationCurveByLastValue(dashEnd, defaultFOV);

        begin_animationDuration = dashBegin.keys[dashBegin.keys.Length - 1].time;
        begin_startValue = dashBegin.keys[0].value;
        begin_startValue = dashBegin.keys[dashBegin.keys.Length - 1].value;

        PublicEvents.OnDash.AddListener(dashAnimation);
    }

    /// <summary>
    /// sets animation curve start time to 0
    /// </summary>
    public static void refitAnimationCurve(AnimationCurve curve)
    {
        Keyframe[] frames = curve.keys;
        float offset = frames[0].time;
        for (int i = 0; i < frames.Length; i++)
        {
            frames[i].time -= offset;
        }
        curve.keys = frames;
    }

    public static void scaleAnimationCurveByFirstValue(AnimationCurve curve, float scalar) 
    {
        Keyframe[] frames = curve.keys;
        float firstValue = frames[0].value;
        for (int i = 0; i < frames.Length; i++)
        {
            frames[i].value = frames[i].value/firstValue * scalar;
        }
        curve.keys = frames;
    }

    public static void scaleAnimationCurveByLastValue(AnimationCurve curve, float startPoint, float endPoint)
    {
        Keyframe[] frames = curve.keys;
        float lastValue = frames[frames.Length-1].value;
        for (int i = 0; i < frames.Length; i++)
        {
            frames[i].value = frames[i].value / lastValue * scalar;
        }
        curve.keys = frames;
    }

    async private void dashAnimation()
    {
        float startTime = Time.time;
        float fov;
        float t;
        while (startTime + begin_animationDuration >= Time.time)
        {
            if (this == null || mainCamera == null) 
                return;

            if (Time.timeScale != 0) // if not paused
            {
                
                fov = dashBegin.Evaluate(Time.time - startTime);
                t = (Time.time - startTime) / begin_animationDuration;
                //main.localPosition = startPos + (Random.insideUnitSphere * intensity);

            }


            await Task.Yield();
        }
        //transform.localPosition = startPos;
    }

    [Button]
    public void DebugRescaleCurves()
    {
        refitAnimationCurve(dashBegin);
        refitAnimationCurve(dashEnd);

        scaleAnimationCurveByFirstValue(dashBegin, GetComponent<Camera>().fieldOfView);
        scaleAnimationCurveByLastValue(dashEnd, GetComponent<Camera>().fieldOfView);
    }
}
