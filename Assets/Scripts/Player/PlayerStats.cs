/*******************************************************************************
 * File Name :         PlayerStats.cs
 * Author(s) :         Toby
 * Creation Date :     3/18/2024
 *
 * Brief Description : its like a scriptable object. but not!
 * have fun designers!
 * might be a bit excessive, but i want small scripts. and variables take up space!
 * 
 * gun stats should be somewhere else
 *****************************************************************************/

using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Input")]
    //[Min(0.01f)]
    //public float MouseSensitivity;
    [Header("Stats")]
    [Min(0)]
    public float DefaultHealth;
    [Tooltip ("Movement speed. legs.")]
    public float Speed;
    [Tooltip("The max target speed")]
    public float MaxSpeed = 20f;
    [Tooltip("The speed of the players dash")]
    public float DashSpeed = 40f;
    [Tooltip("The cool down time in seconds for the player dash")]
    public float DashCoolDown = 3f;
    [Tooltip("How much simulated friction to prevent you from moving forward when stopping input")][Min(0)]
    public float Friction = 0.175f;
    [Tooltip("What percent of normal movement will get applied whilst moving in the air")][Min(0)]
    public float AirMovementMultiplier = 0.5f;
    [Tooltip("How much player regens every second.")][Min(0)]
    public float HealthRegen = 0.25f;
    [Tooltip("Seconds until player heals.")][Min(0)]
    public int SecondsUntilHealing = 2;
    [Header("Jumps")]
    //[Tooltip("how much velocity is carried over from one frame to another")]
    //public float PlayerSlipperyness; //@TODO
    public int AirJumps = 1;
    [Tooltip("How high the player jumps")]
    public float JumpHeight = 2.5f;
    [Tooltip("How much gravity to apply to the player. Normal gravity is not used.")]
    public float GravityBoost = 10f;
}
