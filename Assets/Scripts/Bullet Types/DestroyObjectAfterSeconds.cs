using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectAfterSeconds : MonoBehaviour
{
    [SerializeField] private float Seconds=1;

    public void OnDestroy()
    {
       StopAllCoroutines();
    }
    
    public void Destroy(float seconds)
    {
        StartCoroutine(DestroyObject(seconds));
    }
    IEnumerator DestroyObject(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
