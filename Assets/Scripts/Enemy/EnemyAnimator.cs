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
    private bool dead=false;
    public bool damaged=false;
    public bool attacking = false;
    
    public AnimationState state;
    public enum AnimationState
    {
        Front, Left, Right, Death, Damage, Attack, Top, Back
    }

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
            animator.SetBool("Death",true);
            dead = true;
            return;
        }
        animator.SetTrigger("Damage");
        damaged = true;
    }

    public void OnDamageAnimationEnd()
    {
        damaged = false;
    }

    void LateUpdate()
    {
        if (dead || damaged || attacking)
            return;
        
        float yDiff = player.position.y - rotationReference.position.y;
        if (yDiff > topDownSpriteDistance)
        {
            if (yDiff * yDiff > Mathf.Abs(
                Mathf.Pow(player.position.x - transform.position.x, 2) +
                Mathf.Pow(player.position.z - transform.position.z, 2)))
            {
                if(state != AnimationState.Top)
                    animator.SetTrigger("Top");
                state = AnimationState.Top;
                return;
            }
        }

        if(Vector3.Distance(player.position, transform.position) < playerSpriteChangeDistance)
        {
            if (state != AnimationState.Front)
                animator.SetTrigger("Front");
            state = AnimationState.Front;
            return;
        }

        float angle = transform.eulerAngles.y - rotationReference.eulerAngles.y;
        angle = (angle + 360) % 360;

        //Update sprites
        if (angle < 45 || angle > 315)
        {
            if (state != AnimationState.Front)
                animator.SetTrigger("Front");
            state = AnimationState.Front;
        }
        if (angle > 45 && angle < 135)
        {
            if (state != AnimationState.Right)
                animator.SetTrigger("Right");
            state = AnimationState.Right;
        }
        if (angle > 135 && angle < 225)
        {
            if (state != AnimationState.Back)
                animator.SetTrigger("Back");
            state = AnimationState.Back;
        }
        if (angle > 225 && angle < 315)
        {
            if(state != AnimationState.Left)
                animator.SetTrigger("Left");
            state = AnimationState.Left;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position+transform.forward);
    }
}
