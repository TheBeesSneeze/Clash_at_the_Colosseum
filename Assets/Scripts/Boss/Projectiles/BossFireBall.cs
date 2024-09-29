using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossFireBall : MonoBehaviour
{
    [Tooltip("Only if player collides with the thing")]
    [SerializeField] private float collisionDamage = 60;
    [SerializeField] private float maxHeight=0;
    [SerializeField][Min(0)] private float defaultTime=1;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private Transform visual;

    private void Start()
    {
        PlayerBehaviour p = GameObject.FindObjectOfType<PlayerBehaviour>();
        Launch(transform.position, p.GetGroundPosition());

        Debug.Log(p.GetGroundPosition());
        Debug.Log(p.transform.position);    
    }

    public void Launch(Vector3 startPosition, Vector3 endPosition)
    {
        StartCoroutine(launchArch(startPosition, endPosition, maxHeight, defaultTime));
    }
    public void Launch(Vector3 startPosition, Vector3 endPosition, float height, float time)
    {
        StartCoroutine(launchArch(startPosition, endPosition, height, time));
    }

    /// <summary>
    /// I LOOOOOOOVE BEZIER CURVES
    /// </summary>
    private IEnumerator launchArch(Vector3 startPosition, Vector3 endPosition, float height, float time)
    {
        float timeElapsed=0;
        float t;
        Vector3 arcPosition = (startPosition + endPosition) / 2;
        arcPosition.y += (height * 2);


        while(timeElapsed < time)
        {
            t = timeElapsed/ time;
            Vector3 a = Vector3.Lerp(startPosition, arcPosition, t);
            Vector3 b = Vector3.Lerp(arcPosition, endPosition, t);
            Vector3 position = Vector3.LerpUnclamped(a, b, t);

            Debug.DrawLine(startPosition, arcPosition, Color.red);
            Debug.DrawLine(arcPosition, endPosition, Color.red );
            Debug.DrawLine(a,b, Color.red);

            //transform.rotation.SetLookRotation((position-transform.position).normalized);
            transform.LookAt(position);
            transform.position = position;
            visual.Rotate(new Vector3(0,Time.deltaTime*100,0)); 

            yield return null;

            timeElapsed += Time.deltaTime;
        }

        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        }

        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hello!");
        if(other.TryGetComponent(out PlayerBehaviour player))
        {
            player.TakeDamage(collisionDamage);
            Destroy(this.gameObject);
            return;
        }

        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

}
