///
/// - Toby
///

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.MainMenu
{
    public class HoverMenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] public Vector2 offsetPosition;
        [SerializeField] public Vector2 offsetSize;
        private Vector2 start;
        private Vector2 startPos;

        void Start()
        {
            start = GetComponent<RectTransform>().sizeDelta;
            startPos = GetComponent<RectTransform>().anchoredPosition;
        }

        public void OnPointerEnter(PointerEventData data)
        {
            GetComponent<RectTransform>().sizeDelta = start + offsetSize;
            GetComponent<RectTransform>().anchoredPosition = startPos + offsetPosition;
            //GetComponent<RectTransform>().anchoredPosition = startPos + offsetPosition;
        }

        public void OnPointerExit(PointerEventData data)
        {
            GetComponent<RectTransform>().sizeDelta = start;
            GetComponent<RectTransform>().anchoredPosition = startPos;

            //GetComponent<RectTransform>().anchoredPosition = startPos;
        }
    }
}

