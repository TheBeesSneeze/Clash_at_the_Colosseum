using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceJump : MonoBehaviour
{
    public float speed = 1;
    public float height = 2;
    Transform[] audienceMembers;
    private void Start()
    {
        PublicEvents.OnStageTransition.AddListener(StartAudienceCoroutine);
        Transform[] audienceMembers = GetComponentsInChildren<Transform>();
    }

    private void StartAudienceCoroutine()
    {
        StartCoroutine(AudienceJumping());
    }

    private IEnumerator AudienceJumping()
    {
        bool running = true;
        while (running)
        for (int i = 0; i < audienceMembers.Length; i++)
        {
            float position = Mathf.Sin(Time.time * speed + i) * height / 2;
            Vector3 temp = audienceMembers[i].position;
            temp.y = position;
            audienceMembers[i].position = temp;
        }

        yield break;
    }

}
