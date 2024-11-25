using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;

public class LineControl : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] LineRenderer lineRenderer;
    private float waitTimeWeWant =.5f;
    private float currentWait;

    private void Start()
    {
        currentWait = waitTimeWeWant;
    }
    private void Update()
    {
        if (gameObject != null)
        {
            Despawn();
        }
        currentWait -= Time.deltaTime;
    }
    public void Spawn(Vector3 origin, Vector3 destination, EnemyTakeDamage enemy)
    {
        Debug.Log("Made it to spawn");
        LineRenderer lineRender = GetComponent<LineRenderer>();
        lineRender.SetPosition(0, origin);
        lineRender.SetPosition(1, destination);
        lineRender.enabled = true;
        Instantiate(lineRender);
        
    }

    private void Despawn()
    {
        if(currentWait <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
