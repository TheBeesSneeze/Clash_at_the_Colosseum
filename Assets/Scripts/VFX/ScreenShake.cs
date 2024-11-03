using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;


public class ScreenShake : MonoBehaviour
{
    [SerializeField] private AnimationCurve playerDamageIntensity;
    [SerializeField] private float damageShakeDuration = 0.1f;

    //[SerializeField] private float playerShootIntensity = 0.25f;
    //[SerializeField] private float shootShakeDuration = 0.1f;

    private Transform cameraTransform;
    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
        startPos = cameraTransform.localPosition;

        PublicEvents.OnPlayerDamage.AddListener(OnPlayerTakeDamage);
        //PublicEvents.OnPlayerShoot.AddListener(OnPlayerShoot);
    }

    void OnPlayerTakeDamage()
    {
        DamageShake();
    }

    //void OnPlayerShoot()
    //{
    //}

    async private void DamageShake()
    {
        float startTime = Time.time;
        float intensity;
        float t;
        while(startTime + damageShakeDuration >= Time.time)
        {
            t= (Time.time - startTime)/damageShakeDuration;
            intensity = playerDamageIntensity.Evaluate(Time.time - startTime);
            cameraTransform.localPosition = startPos + (Random.insideUnitSphere * intensity);
            Debug.Log(intensity);
            await Task.Yield();
        }
    }
}
