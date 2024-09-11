using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputEvents : Singleton<InputEvents>
{
    // Events
    [HideInInspector] public UnityEvent MoveStarted, MoveHeld, MoveCanceled;
    [HideInInspector] public UnityEvent ShootStarted, ShootHeld, ShootCanceled;
    [HideInInspector] public UnityEvent GrappleStarted, GrappleHeld, GrappleCanceled;
    [HideInInspector] public UnityEvent JumpStarted, JumpHeld, JumpCanceled;
    [HideInInspector] public UnityEvent SprintStarted, SprintHeld, SprintCanceled;
    [HideInInspector] public UnityEvent PauseStarted, PauseCanceled;
    [HideInInspector] public UnityEvent RestartStarted, RespawnStarted;
    [HideInInspector] public UnityEvent EnemySpawnStarted, EnemySpawnCanceled; 

    // Input values and flags
    public Vector2 LookDelta => Look.ReadValue<Vector2>();
    public Vector3 InputDirection => movementOrigin.TransformDirection(new Vector3(InputDirection2D.x, 0f, InputDirection2D.y));
    public Vector2 InputDirection2D => Move.ReadValue<Vector2>();
    public static bool MovePressed, JumpPressed, ShootPressed;

    public static bool RespawnPressed, PausePressed, GrapplePressed;

    public static bool EnemySpawnPressed;

    private PlayerInput playerInput;
    private InputAction Move, Shoot, Jump, Look, Respawn, Grapple, Pause, SpawnEnemies;

    private Transform movementOrigin;

    private void Start()
    {
        movementOrigin = Camera.main.transform;
        playerInput = GetComponent<PlayerInput>();
        InitializeActions();
    }

    void InitializeActions()
    {
        var map = playerInput.currentActionMap;
        Move = map.FindAction("Move");
        Jump = map.FindAction("Jump");
        Shoot = map.FindAction("Shoot");
        Look = map.FindAction("Look");
        Respawn = map.FindAction("Respawn");
        Grapple = map.FindAction("Grapple");
        Pause = map.FindAction("Pause");
        SpawnEnemies = map.FindAction("Spawn Enemies"); 

        // Subscribe to action events
        Move.started += ctx => ActionStarted(ref MovePressed, MoveStarted);
        Jump.started += ctx => ActionStarted(ref JumpPressed, JumpStarted);
        Shoot.started += ctx => ActionStarted(ref ShootPressed, ShootStarted);
        Pause.started += ctx => { PausePressed = true; PauseStarted.Invoke(); };
        SpawnEnemies.started += ctx => { EnemySpawnPressed = true; EnemySpawnStarted.Invoke(); };
        Grapple.started += ctx => { GrapplePressed = true; GrappleStarted.Invoke(); };

        Move.canceled += ctx => ActionCanceled(ref MovePressed, MoveCanceled);
        Jump.canceled += ctx => ActionCanceled(ref JumpPressed, JumpCanceled);
        Shoot.canceled += ctx => ActionCanceled(ref ShootPressed, ShootCanceled);
        Pause.canceled += ctx => { PausePressed = false; PauseCanceled.Invoke(); };
        SpawnEnemies.canceled += ctx => { EnemySpawnPressed = false; EnemySpawnCanceled.Invoke(); };
        Grapple.canceled += ctx => { GrapplePressed = false; GrappleCanceled.Invoke(); };
    }

    void ActionStarted(ref bool pressedFlag, UnityEvent actionEvent)
    {
        pressedFlag = true;
        actionEvent?.Invoke();
    }

    void ActionCanceled(ref bool pressedFlag, UnityEvent actionEvent)
    {
        pressedFlag = false;
        actionEvent?.Invoke();
    }

    private void Update()
    {
        if (MovePressed) MoveHeld.Invoke();
        if (JumpPressed) JumpHeld.Invoke();
        if (ShootPressed) ShootHeld.Invoke();
        if (GrapplePressed) GrappleHeld.Invoke();
    }

    private void OnDisable()
    {
        // Unsubscribe from all action events to prevent memory leaks
        Move.started -= ctx => ActionStarted(ref MovePressed, MoveStarted);
        Jump.started -= ctx => ActionStarted(ref JumpPressed, JumpStarted);
        Shoot.started -= ctx => ActionStarted(ref ShootPressed, ShootStarted);
        Pause.started -= ctx => { PausePressed = true; PauseStarted.Invoke(); };
        SpawnEnemies.started -= ctx => { EnemySpawnPressed = true; EnemySpawnStarted.Invoke(); };
        Grapple.started -= ctx => { GrapplePressed = true; GrappleStarted.Invoke(); };

        Move.canceled -= ctx => ActionCanceled(ref MovePressed, MoveCanceled);
        Jump.canceled -= ctx => ActionCanceled(ref JumpPressed, JumpCanceled);
        Shoot.canceled -= ctx => ActionCanceled(ref ShootPressed, ShootCanceled);
        Pause.canceled -= ctx => { PausePressed = false; PauseCanceled.Invoke(); };
        SpawnEnemies.canceled -= ctx => { EnemySpawnPressed = false; EnemySpawnCanceled.Invoke(); };
        Grapple.canceled -= ctx => { GrapplePressed = false; GrappleCanceled.Invoke(); };
    }
}
