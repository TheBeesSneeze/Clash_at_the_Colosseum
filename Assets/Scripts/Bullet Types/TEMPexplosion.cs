using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEMPexplosion : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(ExplosionVisual());
    }
    IEnumerator ExplosionVisual()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
