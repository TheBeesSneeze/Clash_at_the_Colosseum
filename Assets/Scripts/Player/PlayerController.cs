/*******************************************************************************
 * File Name :         PlayerController.cs
 * Author(s) :         Toby, Tyler
 * Creation Date :     3/18/2024
 *
 * Brief Description : responds to input events.
 *****************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AudioManager;

[RequireComponent(typeof(PlayerStats))]
public class PlayerController : MonoBehaviour
{
    [Header ("Jump Stats")]
    [SerializeField] private int _airJumps = 1;
    [Tooltip ("How far raycast can see for jumps. Lower = closer to ground before jump. Higher = further off ground before jump")]
    [SerializeField] private float jumpRaycastDistance;
    public bool ConsistentJumps = true;

    [Header ("Camera References")]
    [SerializeField] private Transform cameraHolder;
    [SerializeField] private Transform playerCamera;
    [SerializeField] private Transform playerOrientationTracker;
    [SerializeField] private Transform cameraFollowPoint;

    //references
    private Vector2 input;
    private Rigidbody rb;
    private PlayerStats stats;
    public Rigidbody RB => rb;
    [SerializeField] private LayerMask groundLayers;

    [Header ("Movement")]
    [SerializeField] private float groundDrag;
    [SerializeField] private float airDrag;
    private float xMovement;
    private float yMovement;
    private float horizontalInput;
    private float verticalInput;
    private Vector3 movementDirection;
    

    private int airJumpCounter;

    //audio stuff
    //private float timeSinceLastFootstep = 0;
    //private AudioSource source;

    void Start()
    {
        //groundLayers = new LayerMask();
        //groundLayers |= (1 << LayerMask.GetMask("Default"));
        //groundLayers |= (1 << LayerMask.GetMask("Fill Cell"));
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.useGravity = false;
        stats = GetComponent<PlayerStats>();
        //source = gameObject.AddComponent<AudioSource>();
        //source.volume = 0.15f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        cameraHolder.transform.parent = null;
        InputEvents.Instance.JumpStarted.AddListener(Jump);
        airJumpCounter = stats.AirJumps;
    }

    /// <summary>
    /// every frame while move is held
    /// </summary>
    private void FixedUpdate()
    {
        if (GameManager.Instance.isPaused) return;

        ApplyGravity();


        if (GameManager.Instance.pausedForUI) return;


        DoMovement();
    }

    private void Update()
    {
        if (GameManager.Instance.isPaused) return;
        if (GameManager.Instance.pausedForUI) return;
        UpdateCameraPosition();

        UpdateCameraRotation();
        input = InputEvents.Instance.InputDirection2D;
        horizontalInput = input.x;
        verticalInput = input.y;

        SpeedControl();
        if(IsGrounded())
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = airDrag;
        }
        //FootStepSound();
    }

    /// <summary>
    /// returns true if player is on the ground
    /// </summary>
    /// <returns></returns> bool
    private bool IsGrounded()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, jumpRaycastDistance*2, groundLayers))
        {
            return true;
        }
        return false;
    }

    private void ApplyGravity()
    {
        rb.AddForce(Vector3.down * stats.GravityBoost, ForceMode.Acceleration);
    }

    private void DoMovement()
    {

        //Find actual velocity relative to where player is looking
        Vector2 mag = FindVelRelativeToLook();
        float xMag = mag.x;
        float yMag = mag.y;

        movementDirection = playerOrientationTracker.forward * verticalInput + playerOrientationTracker.right *horizontalInput;

        rb.AddForce(movementDirection.normalized * stats.Speed, ForceMode.Force);
        /*ApplyCounterMovement(input.x, input.y, mag);

        float maxSpeed = stats.MaxSpeed;

        //checks to see if player has reahed max speed
        if (input.x > 0 && xMag > maxSpeed) {
            input.x = 0;
        }
        if (input.x < 0 && xMag < -maxSpeed) {
            input.x = 0;
        }
        if (input.y > 0 && yMag > maxSpeed) {
            input.y = 0;
        }
        if (input.y < 0 && yMag < -maxSpeed) {
            input.y = 0;
        }

        //Apply forces to move player
        rb.AddForce(playerOrientationTracker.forward * (input.y * stats.Speed * Time.deltaTime ));
        rb.AddForce(playerOrientationTracker.right * (input.x * stats.Speed * Time.deltaTime ));*/


    }

    private void SpeedControl()
    {
        Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //limit velocity as needed\
        if(flatVelocity.magnitude > stats.MaxSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * stats.Speed;
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
        }
    }
    /*private void SlidingFix() {
        Vector3 horizontalVelocity = rb.velocity;
        horizontalVelocity.y = 0;
        if(horizontalVelocity.magnitude < 0.01f)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }
    */
    private void Jump()
    {
        if (GrapplingHook.isGrappling)
        {
            return;
        }

        var grav = (Vector3.down * stats.GravityBoost * rb.mass).magnitude;

        if (IsGrounded())
        {
            rb.AddForce(
                Vector3.up * (Mathf.Sqrt(2 * stats.JumpHeight * grav)), ForceMode.Impulse);

            airJumpCounter = _airJumps;

            return;
        }

        if(airJumpCounter > 0)
        {
            rb.AddForce(
                Vector3.up * (Mathf.Sqrt(2 * stats.JumpHeight * grav)), ForceMode.Impulse);

            airJumpCounter--;
        }
    }

    /*private void ApplyCounterMovement(float x, float y, Vector2 mag)
    {
        if (Math.Abs(mag.x) > 0.01f && Math.Abs(x) < 0.05f || (mag.x < -0.01f && x > 0) || (mag.x > 0.01f && x < 0))
        {
            rb.AddForce(playerOrientationTracker.right * (stats.Speed * Time.deltaTime * -mag.x * stats.Friction));
        }
        if (Math.Abs(mag.y) > 0.01f && Math.Abs(y) < 0.05f || (mag.y < -0.01f && y > 0) || (mag.y > 0.01f && y < 0))
        {
            rb.AddForce(playerOrientationTracker.forward * (stats.Speed * Time.deltaTime * -mag.y * stats.Friction));
        }
        if (Mathf.Sqrt((Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.z, 2))) > stats.MaxSpeed)
        {
            float verticalVelocity = rb.velocity.y;
            Vector3 n = rb.velocity.normalized * stats.MaxSpeed;
            rb.velocity = new Vector3(n.x, verticalVelocity, n.z);
        }
    }*/

    public Vector2 FindVelRelativeToLook()
    {
        float lookAngle = playerOrientationTracker.eulerAngles.y;
        float moveAngle = Mathf.Atan2(rb.velocity.x, rb.velocity.z) * Mathf.Rad2Deg;

        float u = Mathf.DeltaAngle(lookAngle, moveAngle);
        float v = 90 - u;

        float mag = new Vector3(rb.velocity.x, 0, rb.velocity.z).magnitude;
        float yMag = mag * Mathf.Cos(u * Mathf.Deg2Rad);
        float xMag = mag * Mathf.Cos(v * Mathf.Deg2Rad);

        return new Vector2(xMag, yMag);
    }

    private void UpdateCameraPosition()
    {
        cameraHolder.position = cameraFollowPoint.position;
    }

    private void UpdateCameraRotation()
    {
       if (GameManager.Instance.isPaused) return;

        var mouse = InputEvents.Instance.LookDelta;
        //float mouseX = mouse.x * OptionInstance.sensitivity * Time.fixedDeltaTime;
        //float mouseY = mouse.y * OptionInstance.sensitivity * Time.fixedDeltaTime;
        float mouseX = mouse.x * 80 * Time.fixedDeltaTime;
        float mouseY = mouse.y * 80 * Time.fixedDeltaTime;
        Vector3 rot = cameraHolder.localRotation.eulerAngles;
        xMovement = rot.y + mouseX;
        yMovement -= mouseY;
        yMovement = Mathf.Clamp(yMovement, -90f, 90f);
        cameraHolder.localRotation = Quaternion.Euler(yMovement, xMovement, 0);
        playerOrientationTracker.localRotation = Quaternion.Euler(0, xMovement, 0);
    }
}