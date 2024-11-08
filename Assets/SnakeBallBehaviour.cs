using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class SnakeBallBehaviour : MonoBehaviour
{
    [Tooltip ("How quickly snake ball deteriorates to 0")]
    [SerializeField] private float durationOfSnakeBall;
    private Vector3 scale;
    private float timeOfSpawn;
    //lerp start, end, editable time
    void Start()
    {
        scale = gameObject.transform.localScale;
        timeOfSpawn = Time.time;
        Deteriorate();
    }

    /// <summary>
    /// causes the snake ball to deteriorite over certain amount of seconds
    /// </summary>
    private async void Deteriorate()
    {
        float timeAlive = Time.time - timeOfSpawn;
        while (timeAlive < durationOfSnakeBall)
        {
            //amount of seconds since spawned in

            timeAlive = Time.time - timeOfSpawn;
            float t = timeAlive / durationOfSnakeBall;
            Vector3 desiredScale = Vector3.Lerp(scale, Vector3.zero, t);
            gameObject.transform.localScale = desiredScale;

            await Task.Yield();
            //^^ YOU CAN DO THIS????
            //new Vector3(Mathf.Lerp(scale.x, 0, t), Mathf.Lerp(scale.y, 0, t), Mathf.Lerp(scale.z, 0, t));
        }

        Destroy(gameObject);
    }
}
