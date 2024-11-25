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

    [SerializeField]
    [Tooltip("This will be the most zoomed in/out the camera will get at the aphex of the animation")]
    private float PeakFOV;

    private float defaultFOV;
    private Camera mainCamera;

    private float begin_animationDuration;
    private float begin_startValue; //values are scaled 
    private float begin_endValue;
    private Coroutine currentAnimation;

    void Start()
    {
        mainCamera = Camera.main;
        defaultFOV = mainCamera.fieldOfView;

        // sets animation time to start at 0
        refitAnimationCurve(dashBegin);
        refitAnimationCurve(dashEnd);

        //scales animation curves start and end at defaultFOV, while maintaining, the original shape
        scaleAnimationCurve(dashBegin, defaultFOV, PeakFOV);
        scaleAnimationCurve(dashEnd, PeakFOV, defaultFOV);

        begin_animationDuration = dashBegin.keys[dashBegin.keys.Length - 1].time;
        begin_startValue = dashBegin.keys[0].value;
        begin_startValue = dashBegin.keys[dashBegin.keys.Length - 1].value;

        PublicEvents.OnDash.AddListener(startDashAnimation);
        PublicEvents.OnDashAvailable.AddListener(endDashAnimation);
    }

    private void startDashAnimation()
    {
        if (currentAnimation != null)
            StopCoroutine(currentAnimation);
        currentAnimation = StartCoroutine(dashAnimation(dashBegin));
    }

    private void endDashAnimation()
    {
        if (currentAnimation != null)
            StopCoroutine(currentAnimation);
        currentAnimation = StartCoroutine(dashAnimation(dashEnd));
    }

    private IEnumerator dashAnimation(AnimationCurve curve)
    {
        
        float animationLength = curve.keys[curve.keys.Length - 1].time - curve.keys[0].time;
        float startTime = Time.time;
        while (startTime + animationLength >= Time.time)
        {
            if (this == null || mainCamera == null)
                break;

            if (Time.timeScale != 0) // if not paused
            {
                float newFOV = curve.Evaluate(Time.time - startTime);
                mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, newFOV, Time.fixedDeltaTime * 16); // smooth with a lerp
            }

            yield return new WaitForFixedUpdate();
        }
        mainCamera.fieldOfView = curve.keys[curve.keys.Length-1].value;
        currentAnimation = null;
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

    /// <summary>
    /// rescales end animation curve
    /// </summary>
    public static void scaleAnimationCurve(AnimationCurve curve, float startPoint, float endPoint)
    {
        Keyframe[] frames = curve.keys;

        float a = frames[0].value;
        float b = frames[frames.Length-1].value;

        for (int i = 1; i < frames.Length-1; i++)
        {
            //there isnt an InverseLerpUnclamped function ig
            float t = (frames[i].value - a)/(b - a);
            frames[i].value = Mathf.LerpUnclamped(startPoint, endPoint,t);
        }

        frames[0].value = startPoint;
        frames[frames.Length - 1].value = endPoint;
        curve.keys = frames;
    }

    [Button]
    public void DebugRescale()
    {
        defaultFOV = Camera.main.fieldOfView;
        scaleAnimationCurve(dashBegin, defaultFOV, PeakFOV);
        scaleAnimationCurve(dashEnd, PeakFOV, defaultFOV);

    }
    
}
