using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceJump : MonoBehaviour
{
    public float speed = 1;
    public float height = 2;
    public float animationTime = 5;
    Transform[] audienceMembers;
    float[] audienceMembersPosition;
    private void Start()
    {
        PublicEvents.OnStageTransition.AddListener(StartAudienceCoroutine);
        audienceMembers = GetComponentsInChildren<Transform>();
        audienceMembersPosition = new float[audienceMembers.Length];

        for (int i = 0; i < audienceMembers.Length; i++)
        {
            audienceMembersPosition[i] = audienceMembers[i].position.y;
        }
    }

    private void StartAudienceCoroutine()
    {
        StartCoroutine(AudienceJumping());
    }

    private IEnumerator AudienceJumping()
    {
        float startTime = Time.time;
        while (animationTime > Time.time - startTime)
        {
            for (int i = 0; i < audienceMembers.Length; i++)
            {
                float position = Mathf.Sin(Time.time * speed + i) * height / 2;
                Vector3 temp = audienceMembers[i].position;
                temp.y = position + audienceMembersPosition[i];
                audienceMembers[i].position = temp;

                Debug.Log("yippie! yippie!");
            }

            yield return null;
        }
 
    }

}
