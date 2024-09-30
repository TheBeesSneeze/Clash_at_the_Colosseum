/*******************************************************************************
* File Name :         ShootingMode.cs
* Author(s) :         Toby Schamberger
* Creation Date :     3/21/2024
*
* Brief Description : scriptable object that determines how the gun works
 *****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "ShootingMode", menuName = "ShootingMode")]
public class ShootingMode : ScriptableObject
{
    [Header("Display")]
    public string GunName;

    [Header("Stats")]
    [Tooltip("RPM of bullets shot")][Min(0.0001f)] 
    public float ShotsPerSecond = 1;
    [Tooltip("# of bullets shot at one time (imagine a shotgun)")][Min(1)] 
    public int BulletsPerShot=1; 
    [Tooltip("(angle) How much to randomize angle (0 is perfect precision)")] [Range(0f, 1f)]
    public float BulletAccuracyOffset=0;
    [Tooltip("The player can hold click to repeat fire")] 
    public bool CanHoldFire=true; //@TODO
    [Tooltip("Speed of the projectile itself (units/second)")] [Min(1)]
    public float BulletSpeed=10;
    [Min(0.1f)]
    public float BulletDamage=1;
    [Tooltip("How much to push player back by. Setting negative pulls player forward")]
    public float RecoilForce;

    [Header("Sprites")]
    public Sprite MenuSprite;
    public GunSprite GameplaySprite;
    public GunAnimation GameplayShootSprite;
    public GunSprite GameplayCantShootSprite;
    public float ShootSpriteSeconds = 0.1f;
    public Color SelectHeaderColor=Color.white;

    [Button]
    public void CalculateDPS()
    {
        if (BulletsPerShot <= 1)
            Debug.Log(ShotsPerSecond * BulletDamage + " damage per second");

        if(BulletsPerShot > 1)
            Debug.Log(ShotsPerSecond * BulletDamage * BulletsPerShot + " DPS across all bullets");
    }
}

[System.Serializable]
public class GunSprite
{
    [SerializeField] private Sprite baseSprite;
    [Header("Effects")]
    [SerializeField] private Sprite bombSprite;
    [SerializeField] private Sprite lightningSprite;
    [SerializeField] private Sprite iceSprite;
    //[SerializeField] private Sprite venomSprite;
    //[SerializeField] private Sprite moreSnakes;
    [SerializeField] private Sprite windSprite;
}

public class GunAnimation: GunSprite
{
    public GunSprite[] sprites;
}

