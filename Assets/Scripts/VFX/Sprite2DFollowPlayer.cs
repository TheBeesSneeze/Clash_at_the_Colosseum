using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*****************************************************************************
// File Name :         Sprite2DFollowPlayer.cs
// Author :            Toby
//
// Brief Description : Four sprite script to visualize enemies
*****************************************************************************/

public class Sprite2DFollowPlayer : MonoBehaviour
{
    [Header ("Sprites")]
    [SerializeField] private Sprite _frontSprite;
    [SerializeField] private Sprite _leftSprite;
    [SerializeField] private Sprite _rightSprite;
    [SerializeField] private Sprite _backSprite;

    [Header ("Base Enemy Transform")]
    [SerializeField] private Transform rotationReference;

    private Transform player;
    private SpriteRenderer _sprite;

    // Use this for initialization
    void Start()
    {
        if(rotationReference == null)
            rotationReference = transform.parent;
        player = GameObject.FindObjectOfType<PlayerBehaviour>().transform;
        _sprite = GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        float angle = transform.eulerAngles.y-rotationReference.eulerAngles.y;
        angle = (angle + 360) % 360;

        //Update sprites
        if (angle < 45 || angle > 315)
            _sprite.sprite = _frontSprite;
        if (angle > 45 && angle < 135)
            _sprite.sprite = _rightSprite;
        if(angle > 135 && angle < 225)
            _sprite.sprite = _backSprite;
        if(angle > 225 && angle < 315)
            _sprite.sprite = _leftSprite;
    }

    private void FixedUpdate()
    {
        transform.LookAt(player.position);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + transform.parent.forward);
    }
}
