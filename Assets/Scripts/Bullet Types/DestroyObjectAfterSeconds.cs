using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectAfterSeconds : MonoBehaviour
{
    public void Destroy(float seconds=1)
    {
        StartCoroutine(ExplosionVisual(seconds));
    }
    IEnumerator ExplosionVisual(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
