using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectAfterSeconds : MonoBehaviour
{
    [SerializeField] private float Seconds=1;

    public void OnDestroy()
    {
        StartCoroutine(ExplosionVisual(Seconds));
    }
    public void Destroy(float seconds)
    {
        StartCoroutine(ExplosionVisual(seconds));
    }
    IEnumerator ExplosionVisual(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
