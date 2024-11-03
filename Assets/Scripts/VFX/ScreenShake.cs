///
/// Uses animation curve 
///


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;


public class ScreenShake : MonoBehaviour
{
    [Tooltip("x axis (time) needs to be synced with damageShakeDuration")]
    [SerializeField] private AnimationCurve playerDamageIntensityCurve;
    private float damageShakeDuration = 0.1f;

    //[SerializeField] private float playerShootIntensity = 0.25f;
    //[SerializeField] private float shootShakeDuration = 0.1f;

    private Transform cameraTransform;
    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
        startPos = cameraTransform.localPosition;

        //automatically get animation time
        damageShakeDuration = playerDamageIntensityCurve.keys[playerDamageIntensityCurve.keys.Length - 1].time;

        PublicEvents.OnPlayerDamage.AddListener(DamageShake);
        //PublicEvents.OnPlayerShoot.AddListener(OnPlayerShoot);
    }

    async private void DamageShake()
    {
        float startTime = Time.time;
        float intensity;
        float t;
        while(startTime + damageShakeDuration >= Time.time)
        {
            t= (Time.time - startTime)/damageShakeDuration;
            intensity = playerDamageIntensityCurve.Evaluate(Time.time - startTime);
            cameraTransform.localPosition = startPos + (Random.insideUnitSphere * intensity);
            Debug.Log(intensity);
            await Task.Yield();
        }
    }
}
