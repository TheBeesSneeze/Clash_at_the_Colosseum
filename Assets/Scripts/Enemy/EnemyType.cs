/*******************************************************************************
* File Name :         EnemyType.cs
* Author(s) :         Toby Schamberger
* Creation Date :     3/20/2024
*
* Brief Description : 
* Contains universal stats about enemies. 
* Does not contain any movement AI or decision AI.
 *****************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using static AudioManager;

public class EnemyType : CharacterType
{
    public float DefaultHealth = 1;
    private Slider slider;
    private bool canDamage = false;
    [SerializeField] private float iFrameSeconds = 1;
    [SerializeField] private int enemyDamage = 1;
    private PlayerBehaviour player;
    private Coroutine playerIFrames;
    [Tooltip ("What color the enemy is when it takes damage")]
    [SerializeField] private Color enemyDamageColor;
    private MeshRenderer meshRenderer;
    [Tooltip("Enemy damage flash time")]
    [SerializeField] private float enemyDamageFlashSeconds = 0.5f;
    private Color enemyOriginalColor;

    protected override void Start()
    {
        base.Start();
        CurrentHealth = DefaultHealth;
        slider = GetComponentInChildren<Slider>();
        meshRenderer = GetComponent<MeshRenderer>();
        enemyOriginalColor = meshRenderer.material.color;
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        if (slider != null)
        {
            float t = CurrentHealth / DefaultHealth;
            slider.value = t;
        }

        StartCoroutine(EnemyDamageFlash());
    }

    public IEnumerator EnemyDamageFlash()
    {
        meshRenderer.material.color = enemyDamageColor;
        Debug.Log("setting color");
        yield return new WaitForSeconds(enemyDamageFlashSeconds);
        meshRenderer.material.color = enemyOriginalColor;
        Debug.Log("normal color");
    }

    private void OnCollisionEnter(Collision collision)
    {

        var p = collision.gameObject.GetComponent<PlayerBehaviour>();

        if (p != null)
        {
            player = collision.gameObject.GetComponent<PlayerBehaviour>();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        var p = collision.gameObject.GetComponent<PlayerBehaviour>();
    }

    public override void Die()
    {
        base.Die();
    }
}
