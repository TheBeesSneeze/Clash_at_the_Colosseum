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
        startPos = cameraTransform.position;

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
        bool shook = TryDamageShake() && TryShootShake();

        if(!shook)
            cameraTransform.position = startPos;
    }

    bool TryDamageShake()
    {
        if (damageShakeTimer > 0)
        {
            damageShakeTimer-= Time.deltaTime;
            cameraTransform.position = startPos + (Random.insideUnitSphere * playerShootIntensity);
            return true;
        }
        return false;
    }

    bool TryShootShake()
    {
        if (shootShakeTimer > 0)
        {
            shootShakeTimer -= Time.deltaTime;
            cameraTransform.position = startPos + (Random.insideUnitSphere * playerDamageIntensity);
            return true;
        }
        return false;
    }
}
