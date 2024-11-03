using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    [SerializeField] private float playerDamageIntensity = 0.5f;
    [SerializeField] private float damageShakeDuration = 0.1f;
    private float damageShakeTimer = 0;

    [SerializeField] private float playerShootIntensity = 0.25f;
    [SerializeField] private float shootShakeDuration = 0.1f;
    private float shootShakeTimer=0;

    private Transform cameraTransform;
    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
        startPos = cameraTransform.localPosition;

<<<<<<< Updated upstream
        PublicEvents.OnPlayerDamage.AddListener(OnPlayerTakeDamage);
        PublicEvents.OnPlayerShoot.AddListener(OnPlayerShoot);
    }

    void OnPlayerTakeDamage()
    {
        shootShakeTimer = shootShakeDuration;
    }

    void OnPlayerShoot()
    {
        damageShakeTimer = damageShakeDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.pausedForUI || GameManager.Instance.isPaused)
            return;

        bool shook = TryDamageShake() || TryShootShake();

        if(!shook)
            cameraTransform.localPosition = startPos;
    }

    bool TryDamageShake()
    {
        if (damageShakeTimer > 0)
        {
            damageShakeTimer-= Time.deltaTime;
            cameraTransform.localPosition = startPos + (Random.insideUnitSphere * playerShootIntensity);
            return true;
=======
        PublicEvents.OnPlayerDamage.AddListener(DamageShakeAnimation);
        //PublicEvents.OnPlayerShoot.AddListener(OnPlayerShoot);
    }

    //void OnPlayerShoot()
    //{
    //}

    async private void DamageShakeAnimation()
    {
        float startTime = Time.time;
        float intensity;
        while(startTime + damageShakeDuration >= Time.time)
        {
            intensity = playerDamageIntensity.Evaluate(Time.time - startTime); 
            cameraTransform.localPosition = startPos + (Random.insideUnitSphere * intensity);
            Debug.Log(intensity);
            await Task.Yield();
>>>>>>> Stashed changes
        }
        return false;
    }

    bool TryShootShake()
    {
        if (shootShakeTimer > 0)
        {
            //oDebug.Log("screen shake " + shootShakeTimer);
            shootShakeTimer -= Time.deltaTime;
            cameraTransform.localPosition = startPos + (Random.insideUnitSphere * playerDamageIntensity);
            return true;
        }
        return false;
    }
}
