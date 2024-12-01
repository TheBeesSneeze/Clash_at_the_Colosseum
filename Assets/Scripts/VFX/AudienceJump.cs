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
        PublicEvents.HydraDeath.AddListener(StartAudienceForever);


        audienceMembers = GetComponentsInChildren<Transform>();
        audienceMembersPosition = new float[audienceMembers.Length];

        for (int i = 0; i < audienceMembers.Length; i++)
        {
            audienceMembersPosition[i] = audienceMembers[i].localPosition.y;
        }
    }

    private void StartAudienceCoroutine()
    {
        StartCoroutine(AudienceJumping(animationTime));
    }

    private void StartAudienceForever()
    {
        StartCoroutine(AudienceJumping(9999));
    }

    private IEnumerator AudienceJumping(float jumpTime)
    {
        int j = 0;
        float startTime = Time.time;
        while (jumpTime > Time.time - startTime)
        {
            j++;
            for (int i = 0; i < audienceMembers.Length; i++)
            {
                if(i%2==j%2) //only do the thing 1/4 the time i think
                {
                    float position = Mathf.Sin(Time.time * speed + i) * height / 2;
                    Vector3 temp = audienceMembers[i].localPosition;
                    temp.y = position + audienceMembersPosition[i];
                    audienceMembers[i].localPosition = temp;
                }
                
            }

            yield return new WaitForFixedUpdate();
        }
 
    }

}
