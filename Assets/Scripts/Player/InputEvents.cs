using System.Net.Http.Headers;
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
    [HideInInspector] public UnityEvent DashStarted, DashHeld, DashCanceled;
    [HideInInspector] public UnityEvent PauseStarted, PauseCanceled;
    [HideInInspector] public UnityEvent RestartStarted, RespawnStarted;
    [HideInInspector] public UnityEvent HealStarted, HealCanceled;
    [HideInInspector] public UnityEvent ReloadStarted, ReloadCanceled;

    private float _sensitivity=1;

    // Input values and flags
    public Vector2 LookDelta => Look.ReadValue<Vector2>() * _sensitivity;
    public Vector3 InputDirection => movementOrigin.TransformDirection(new Vector3(InputDirection2D.x, 0f, InputDirection2D.y));
    public Vector2 InputDirection2D => Move.ReadValue<Vector2>();
    public static bool MovePressed, JumpPressed, ShootPressed, RespawnPressed, DashPressed,
        PausePressed, GrapplePressed, HealPressed, ReloadPressed;

    private PlayerInput playerInput;
    private InputAction Move, Shoot, Jump, Look, Respawn, Grapple, Dash, Pause, Heal, Reload;

    private Transform movementOrigin;

    private void Start()
    {
        movementOrigin = Camera.main.transform;
        playerInput = GetComponent<PlayerInput>();
        InitializeActions();
        _sensitivity = PlayerPrefs.GetFloat("sensitivity", GameManager.Instance.DefaultSensitivity);
        PublicEvents.OnSensitivitySliderChanged.AddListener(OnSensitivitySliderChange);
    }

    void InitializeActions()
    {
        var map = playerInput.currentActionMap;
        Move = map.FindAction("Move");
        Jump = map.FindAction("Jump");
        Shoot = map.FindAction("Shoot");
        Look = map.FindAction("Look");
        Respawn = map.FindAction("Respawn");
        Dash = map.FindAction("Dash");
        Grapple = map.FindAction("Grapple");
        Pause = map.FindAction("Pause");
        Heal = map.FindAction("Heal");
        Reload = map.FindAction("Reload");



        Move.started += ctx => ActionStarted(ref MovePressed, MoveStarted);
        Jump.started += ctx => ActionStarted(ref JumpPressed, JumpStarted);
        Shoot.started += ctx => ActionStarted(ref ShootPressed, ShootStarted);
        Pause.started += ctx => { PausePressed = true; PauseStarted.Invoke(); };
        Dash.started += ctx => ActionCanceled(ref DashPressed, DashStarted);
        Heal.started += ctx => ActionCanceled(ref HealPressed, HealStarted);
        Reload.started += ctx => ActionStarted(ref ReloadPressed, ReloadStarted); 
        Grapple.started += ctx => ActionCanceled(ref GrapplePressed, GrappleStarted);

        Move.canceled += ctx => ActionCanceled(ref MovePressed, MoveCanceled);
        Jump.canceled += ctx => ActionCanceled(ref JumpPressed, JumpCanceled);
        Shoot.canceled += ctx => ActionCanceled(ref ShootPressed, ShootCanceled);
        Pause.canceled += ctx => { PausePressed = false; PauseCanceled.Invoke(); };
        Dash.canceled += ctx => ActionCanceled(ref DashPressed, DashCanceled);
        Heal.canceled += ctx => ActionCanceled(ref HealPressed, HealCanceled);
        Reload.canceled += ctx => ActionCanceled(ref ReloadPressed, ReloadCanceled);
        Grapple.canceled += ctx => ActionCanceled(ref GrapplePressed, GrappleCanceled);
    }
    void ActionStarted(ref bool pressedFlag, UnityEvent actionEvent)
    {
        if (GameManager.Instance.isPaused) return;
        if (GameManager.Instance.pausedForUI) return;
        pressedFlag = true;
        actionEvent?.Invoke();
    }
    void ActionCanceled(ref bool pressedFlag, UnityEvent actionEvent)
    {
        if (GameManager.Instance.isPaused) return;
        if (GameManager.Instance.pausedForUI) return;


        pressedFlag = false;
        actionEvent?.Invoke();
    }
    private void Update()
    {
        if (GameManager.Instance.isPaused) return;
        if (GameManager.Instance.pausedForUI) return;

        if (MovePressed) MoveHeld.Invoke();
        if (JumpPressed) JumpHeld.Invoke();
        if (ShootPressed) ShootHeld.Invoke();
        if (GrapplePressed) GrappleHeld.Invoke();
    }
    private void OnDisable()
    {
        // Unsubscribe from all action events to prevent memory leaks
        /*Move.started -= ctx => ActionStarted(ref MovePressed, MoveStarted);
        Jump.started -= ctx => ActionStarted(ref JumpPressed, JumpStarted);
        Shoot.started -= ctx => ActionStarted(ref ShootPressed, ShootStarted);
        Pause.started -= ctx => ActionCanceled(ref PausePressed, PauseStarted);
        Dash.started -= ctx => ActionCanceled(ref DashPressed, DashStarted);
        Heal.started -= ctx => ActionCanceled(ref HealPressed, HealStarted);
        Grapple.started -= ctx => ActionCanceled(ref GrapplePressed, GrappleStarted);
        Reload.started -= ctx => ActionCanceled(ref ReloadPressed, ReloadStarted);  

        Move.canceled -= ctx => ActionCanceled(ref MovePressed, MoveCanceled);
        Jump.canceled -= ctx => ActionCanceled(ref JumpPressed, JumpCanceled);
        Shoot.canceled -= ctx => ActionCanceled(ref ShootPressed, ShootCanceled);
        Pause.canceled -= ctx => ActionCanceled(ref PausePressed, PauseCanceled);
        Dash.canceled -= ctx => ActionCanceled(ref DashPressed, DashCanceled);
        Heal.canceled -= ctx => ActionCanceled(ref HealPressed, HealCanceled);
        Grapple.canceled -= ctx => ActionCanceled(ref GrapplePressed, GrappleCanceled);
        Reload.started -= ctx => ActionCanceled(ref ReloadPressed, ReloadCancled);*/
        Move.started -= ctx => ActionStarted(ref MovePressed, MoveStarted);
        Jump.started -= ctx => ActionStarted(ref JumpPressed, JumpStarted);
        Shoot.started -= ctx => ActionStarted(ref ShootPressed, ShootStarted);
        Pause.started -= ctx => { PausePressed = true; PauseStarted.Invoke(); };
        Dash.started -= ctx => ActionCanceled(ref DashPressed, DashStarted);
        Heal.started -= ctx => ActionCanceled(ref HealPressed, HealStarted);
        if(Reload != null)
            Reload.started -= ctx => ActionStarted(ref ReloadPressed, ReloadStarted);
        Grapple.started -= ctx => ActionCanceled(ref GrapplePressed, GrappleStarted);

        Move.canceled -= ctx => ActionCanceled(ref MovePressed, MoveCanceled);
        Jump.canceled -= ctx => ActionCanceled(ref JumpPressed, JumpCanceled);
        Shoot.canceled -= ctx => ActionCanceled(ref ShootPressed, ShootCanceled);
        Pause.canceled -= ctx => { PausePressed = false; PauseCanceled.Invoke(); };
        Dash.canceled -= ctx => ActionCanceled(ref DashPressed, DashCanceled);
        Heal.canceled -= ctx => ActionCanceled(ref HealPressed, HealCanceled);
        Reload.canceled -= ctx => ActionCanceled(ref ReloadPressed, ReloadCanceled);
        Grapple.canceled += ctx => ActionCanceled(ref GrapplePressed, GrappleCanceled);
    }

    private void OnSensitivitySliderChange()
    {
        _sensitivity = PlayerPrefs.GetFloat("sensitivity", GameManager.Instance.DefaultSensitivity);
    }
}
