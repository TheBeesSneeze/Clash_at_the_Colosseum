using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class DashScript : MonoBehaviour
{
    public GameObject facing;
    bool dashCooldown = false;
    Rigidbody rb;
    PlayerStats stats;
    PlayerBehaviour _playerBehaviour;
    PlayerController _playerController;
    Camera _mainCamera;

    private bool canDash=true;
    void Start()
    {
        InputEvents.Instance.DashStarted.AddListener(startDash);
        rb = gameObject.GetComponent<Rigidbody>();
        stats = GetComponent<PlayerStats>();
        _playerBehaviour = gameObject.GetComponent<PlayerBehaviour>();
        _playerController = gameObject.GetComponent<PlayerController>();
        _mainCamera = Camera.main;
    }
    public void startDash() 
    {
        if (canDash) 
        {
            Vector3 direction = InputEvents.Instance.InputDirection;

            if (direction == Vector3.zero) //if player isnt pressing any keys
                direction = _mainCamera.transform.forward;

            _playerBehaviour.canTakeDamage = false;
            rb.AddForce(direction.normalized * stats.DashSpeed);
            PublicEvents.OnDash.Invoke();
            dashCooldown = true;
            StartCoroutine(DashCoolDown());
            StartCoroutine(DashInvincibilityTime());
        }
    }
    IEnumerator DashInvincibilityTime()
    {
        yield return new WaitForSeconds(stats.DashInvincibilityTime);
        _playerBehaviour.canTakeDamage = true;
    }
    IEnumerator DashCoolDown()
    {
        yield return new WaitForSeconds(stats.DashCoolDown);
        
        //wait for player to not be grounded
        while(!_playerController.IsGrounded())
            yield return null;

        canDash = true;
        PublicEvents.OnDashAvailable?.Invoke();
        
    }
}
