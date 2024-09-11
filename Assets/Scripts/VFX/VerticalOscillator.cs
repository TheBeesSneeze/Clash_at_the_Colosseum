using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*****************************************************************************
// File Name :         VerticalOscillator.cs
// Author :            Toby
//
// Brief Description : up and down and up and down and
*****************************************************************************/

public class VerticalOscillator : MonoBehaviour
{
    [SerializeField] private float _yMin = -1;
    [SerializeField] private float _yMax = 1;
    [SerializeField] private float _speed = 1;
    [Tooltip("Defaults to this transform")]
    [SerializeField] private Transform _targetObject;



    private void Start()
    {
        if(_targetObject == null)
            _targetObject = transform;
    }

    private void Update()
    {
        Vector3 pos = _targetObject.localPosition;
        float t = (Mathf.Sin(Time.time) + 1) / 2;
        float y = Mathf.Lerp(_yMin, _yMax, t);
        pos.y = y;
        _targetObject.localPosition = pos;
    }
}
