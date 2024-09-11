///
/// - Toby
///

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace mainMenu
{
    public class HoverMenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] public Vector2 offset;
        private Vector2 start;
        private Vector2 startPos;

        void Start()
        {
            start = GetComponent<RectTransform>().sizeDelta;
            startPos = GetComponent<RectTransform>().anchoredPosition;
            Debug.Log(start);
        }

        public void OnPointerEnter(PointerEventData data)
        {
            GetComponent<RectTransform>().sizeDelta = start+offset;
            GetComponent<RectTransform>().anchoredPosition += offset;
        }

        public void OnPointerExit(PointerEventData data)
        {
            GetComponent<RectTransform>().sizeDelta = start;
        }
    }
}

