using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DashScript : MonoBehaviour
{
    public GameObject facing;
    bool dashCooldown = false;
    Rigidbody rb;
    PlayerStats stats;
    PlayerBehaviour player;
    void Start()
    {
        InputEvents.Instance.DashStarted.AddListener(startDash);
        InputEvents.Instance.MoveStarted.AddListener(getKeyPressed);
        rb = gameObject.GetComponent<Rigidbody>();
        stats = GetComponent<PlayerStats>();
        player = gameObject.GetComponent<PlayerBehaviour>();
    }
    public void getKeyPressed() { 
    }
    public void startDash() {
        Vector3 direction = InputEvents.Instance.InputDirection;
        if (!dashCooldown) {
            player.canTakeDamage = false;
            rb.AddForce(direction * stats.DashSpeed);
            PublicEvents.OnDash.Invoke();
            dashCooldown = true;
            StartCoroutine(DashCoolDown());
            StartCoroutine(DashInvincibilityTime());
        }
    }
    IEnumerator DashInvincibilityTime()
    {
        yield return new WaitForSeconds(stats.DashInvincibilityTime);
        player.canTakeDamage = true;
    }
    IEnumerator DashCoolDown()
    {
        yield return new WaitForSeconds(stats.DashCoolDown);
        dashCooldown = false;
    }
}
