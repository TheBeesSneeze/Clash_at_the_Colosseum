using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DashScript : MonoBehaviour
{
    public float coolDownTime;
    public GameObject facing;
    bool dashCooldown = false;
    Rigidbody rb;
    PlayerStats stats;
    void Start()
    {
        InputEvents.Instance.DashStarted.AddListener(startDash);
        rb = gameObject.GetComponent<Rigidbody>();
        stats = GetComponent<PlayerStats>();
    }
    public void startDash() {
        Vector3 direction = facing.GetComponent<Transform>().forward;
        if (!dashCooldown) {
            rb.AddForce(direction * stats.DashSpeed);
        }
        StartCoroutine(DashCoolDown());
    }
    IEnumerator DashCoolDown()
    {
        dashCooldown = true;
        yield return new WaitForSeconds(stats.DashCoolDown);
        dashCooldown = false;
    }
}
