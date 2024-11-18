using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Player
{
public class DashScript : MonoBehaviour
{
    public GameObject facing;
    bool dashCooldown = false;
    Rigidbody rb;
    PlayerStats stats;
    PlayerBehaviour pb;
    PlayerController pc;
    public bool recentlyGrounded;
    void Start()
    {
        InputEvents.Instance.DashStarted.AddListener(startDash);
        InputEvents.Instance.MoveStarted.AddListener(getKeyPressed);
        rb = gameObject.GetComponent<Rigidbody>();
        stats = GetComponent<PlayerStats>();
        pb = gameObject.GetComponent<PlayerBehaviour>();
        pc = gameObject.GetComponent<PlayerController>();
    }
    public void getKeyPressed()
    {

    }
    public void startDash()
    {
        Vector3 direction = InputEvents.Instance.InputDirection;
        if (!dashCooldown && recentlyGrounded)
        {
            pb.canTakeDamage = false;
            rb.AddForce(direction * stats.DashSpeed);
            PublicEvents.OnDash.Invoke();
            dashCooldown = true;
            StartCoroutine(DashCoolDown());
            StartCoroutine(DashInvincibilityTime());
            recentlyGrounded = pc.IsGrounded();
        }
    }
    IEnumerator DashInvincibilityTime()
    {
        yield return new WaitForSeconds(stats.DashInvincibilityTime);
        pb.canTakeDamage = true;
    }
    IEnumerator DashCoolDown()
    {
        yield return new WaitForSeconds(stats.DashCoolDown);
        dashCooldown = false;
    }
}
}

