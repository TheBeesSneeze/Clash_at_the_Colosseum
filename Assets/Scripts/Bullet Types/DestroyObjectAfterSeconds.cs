using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectAfterSeconds : MonoBehaviour
{
    [SerializeField] private float Seconds=1;
    [SerializeField] private bool DestroyAtStart=false;

    public void OnDestroy()
    {
       StopAllCoroutines();
    }

    public void Start()
    {
        if(DestroyAtStart)
            DestroyTimer(Seconds);
    }

    public void DestroyTimer(float seconds)
    {
        StartCoroutine(DestroyObject(seconds));
    }
    IEnumerator DestroyObject(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
