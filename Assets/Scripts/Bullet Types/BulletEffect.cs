/*******************************************************************************
 * File Name :         BulletEffect.cs
 * Author(s) :        Alec, Toby, Sky
 * Creation Date :     3/22/2024
 *
 * Brief Description : Scriptable Object base for handling Enemy Logic
 *****************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
public abstract class BulletEffect : ScriptableObject
{
    [Header("Gameplay")]
    public Color TrailColor = Color.white;
    //this is the longest tooltip in the game
    [Tooltip("*IF* the bullet effect has a component that does damage," +
            " that damage is determined by multiplying the current " +
            "shootmodes damage (per bullet) by this number")]
    public float DamageMultiplier; //you might have to implement this manually on a effect by effect basis. sorry guys
    [InfoBox("Setting any bullet effect to be destroyed = false will override every other effect")]
    public bool DestroyBulletOnSurfaceContact=true;
    [Tooltip("Entity refers to player OR enemies")]
    public bool DestroyBulletOnEntityContact=true;

    [Header("UI")]
    public string UpgradeName;
    public Sprite UpgradeIcon;
    public Color bodyColor=Color.white;
    public Color secondaryColor=Color.white;

    protected Bullet _parentBullet;
    protected GunController _playerGunController;

    /// <summary>
    /// Really trying hard to come up with a better name for this one. uhhh come back to me
    /// </summary>
    public void DefaultInitialize(Bullet parentBullet, GunController player)
    {
        _parentBullet = parentBullet;
        _playerGunController = player;
    }

    public abstract void Initialize();
    public abstract void OnEnemyHit(EnemyTakeDamage type, float damage);
    public abstract void OnHitOther(Vector3 point, float damage);

}