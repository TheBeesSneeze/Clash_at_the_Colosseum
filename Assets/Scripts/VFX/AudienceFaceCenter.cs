using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceFaceCenter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OrientAllChildren () //like a youth pastor
    {
        Transform[] sprites = GetComponentsInChildren<Transform>();
        foreach (Transform t in sprites)
        {
            //Vector3 direction = Vector3.zero - t.position;
            Vector3 center = new Vector3(0, t.position.y, 0);
            t.LookAt(center);
        }
    }

    private void OnDrawGizmosSelected()
    {
        OrientAllChildren();
    }
}
