/*******************************************************************************
* File Name :         ShootingMode.cs
* Author(s) :         Toby Schamberger, Clare Grady 
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
    [Tooltip("How long it takes for the gun to reload")]
    public float ReloadSpeed;
    [Tooltip("How many shots the gun has before it needs to reload")]
    public int ClipSize;
    [Tooltip("If the gun can shoot infinetly without reloading")]
    public bool canInfiniteFire;

    [Header("Sprites")]
    public Sprite MenuSprite;
    public GunAnimation GameplaySprite;
    public GunAnimation GameplayShootSprite;
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
    [Header("Base Gun")]
    [SerializeField] public Sprite AllSnakes;
    [SerializeField] public Sprite TwoSnakes;
    [SerializeField] public Sprite OneSnakes;
    [SerializeField] public Sprite NoneSnakes;
    [Header("Effects")]
    [SerializeField] public Sprite bouncingSprite;
    [SerializeField] public Sprite bombSprite;
    [SerializeField] public Sprite lightningSprite;
    [SerializeField] public Sprite iceSprite;
    [SerializeField] public Sprite venomSprite;
    //[SerializeField] public Sprite moreSnakes;
    [SerializeField] public Sprite windSprite;
}
[System.Serializable]
public class GunAnimation
{
    public List<GunSprite> sprites;
    public float SecondsBetweenFrames = 0.1f;
}

