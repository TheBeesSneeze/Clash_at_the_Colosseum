using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace mainMenu
{
    public class ScrollUVRect : MonoBehaviour
    {
        [SerializeField] private Vector2 direction;
        [SerializeField] private float speed;
        private RawImage _image;
        private Vector2 uv;

        // Start is called before the first frame update
        void Start()
        {
            _image = GetComponent<RawImage>();
        }

        // Update is called once per frame
        void Update()
        {
            uv += direction * (speed * Time.deltaTime);
            //_image.uvRect.position = uv; doesnt work for no fucking reason
            var r = _image.uvRect;
            r.position = uv;
        }
    }
}