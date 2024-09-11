using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DashScript : MonoBehaviour
{
    public float coolDownTime;
    public float dashSpeed;
    bool dashCooldown = false;
    Rigidbody rb;
    Vector2 input;
    PlayerStats stats;
    void Start()
    {
        print("dash script is doing somthing");
        InputEvents.Instance.SprintStarted.AddListener(startDash);
        rb = gameObject.GetComponent<Rigidbody>();
        input = InputEvents.Instance.InputDirection2D;
        stats = GetComponent<PlayerStats>();
    }
    public void startDash() {
        print("shift pressed");
        if (!dashCooldown) {
            rb.AddForce(Vector3.forward * (input.y * stats.DashSpeed * Time.deltaTime));
        }
        StartCoroutine(DashCoolDown());
    }
    IEnumerator DashCoolDown()
    {
        dashCooldown = true;
        yield return new WaitForSeconds(coolDownTime);
        dashCooldown = false;
    }
}
