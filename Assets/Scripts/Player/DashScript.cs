using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DashScript : MonoBehaviour
{
    public GameObject facing;
    bool dashCooldown = false;
    Rigidbody rb;
    PlayerStats stats;
    void Start()
    {
        InputEvents.Instance.DashStarted.AddListener(startDash);
        InputEvents.Instance.MoveStarted.AddListener(getKeyPressed);
        rb = gameObject.GetComponent<Rigidbody>();
        stats = GetComponent<PlayerStats>();
    }
    public void getKeyPressed() { 
    }
    public void startDash() {
        Vector3 direction = InputEvents.Instance.InputDirection;
        if (!dashCooldown) {
            rb.AddForce(direction * stats.DashSpeed);
            PublicEvents.OnDash.Invoke();
            dashCooldown = true;
            StartCoroutine(DashCoolDown());
        }
    }
    IEnumerator DashCoolDown()
    {
        yield return new WaitForSeconds(stats.DashCoolDown);
        dashCooldown = false;
    }
}
