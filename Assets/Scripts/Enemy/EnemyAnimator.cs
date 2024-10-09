using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{

    [SerializeField] private Animator animator;
    [SerializeField] private float topDownSpriteDistance=3;
    [Tooltip("goes into front face sprite mode when within distance")]
    [SerializeField] private float playerSpriteChangeDistance;
    [SerializeField] private Transform rotationReference;

    private static Transform player;
    private bool dead;
    public bool damaged;
    public bool attacking;

    // Use this for initialization
    void Start()
    {
        if(animator == null)
            animator = GetComponent<Animator>();
        if(rotationReference == null)
            rotationReference = transform;
        if (player == null)
            player = GameObject.FindObjectOfType<PlayerBehaviour>().transform;
    }

    /// <summary>
    /// called in enemytakedamage
    /// </summary>
    public void OnTakeDamage(float newHealth)
    {
        if(newHealth < 0)
        {
            animator.SetTrigger("Death");
            dead = true;
            return;
        }
        animator.SetTrigger("Damage");
    }

    void LateUpdate()
    {
        if (dead || damaged || attacking)
            return;
        
        float angle = transform.eulerAngles.y-rotationReference.eulerAngles.y;
        angle = (angle + 360) % 360;

        float yDiff = player.position.y - rotationReference.position.y;
        if (yDiff > topDownSpriteDistance)
        {
            if (yDiff * yDiff > Mathf.Abs(
                Mathf.Pow(player.position.x - transform.position.x, 2) +
                Mathf.Pow(player.position.y - transform.position.y, 2)))
            {
                animator.SetTrigger("Top");
                return;
            }
        }

        if(Vector3.Distance(player.position, transform.position) < playerSpriteChangeDistance)
        {
            animator.SetTrigger("Front");
            return;
        }


        //Update sprites
        if (angle < 45 || angle > 315)
            animator.SetTrigger("Front");
        if (angle > 45 && angle < 135)
            animator.SetTrigger("Right");
        if (angle > 135 && angle < 225)
            animator.SetTrigger("Bac");
        if (angle > 225 && angle < 315)
            animator.SetTrigger("Left");

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + transform.parent.forward);
    }
}
