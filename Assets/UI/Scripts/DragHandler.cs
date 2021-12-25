using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI {
    public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
        public static GameObject DragItem;
        private Transform _startParent;
        private Transform _canvas;

        public void OnBeginDrag(PointerEventData eventData) {
            DragItem = Instantiate(gameObject, transform.position, Quaternion.identity, _canvas);

            SetAbsoluteRectTransform(transform.GetComponent<RectTransform>());

            _startParent = DragItem.transform.parent;

            DragItem.GetComponent<Image>().raycastTarget = false;
            DragItem.GetComponent<DragHandler>()._canvas = _canvas;
        }

        public void OnDrag(PointerEventData eventData) {
            DragItem.transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData) {
            DragItem.GetComponent<Image>().raycastTarget = true;

            if (_startParent == DragItem.transform.parent) {
                Destroy(DragItem);
            }
            
            DragItem = null;
        }

        private static void SetAbsoluteRectTransform(RectTransform from) {
            RectTransform current = DragItem.transform.GetComponent<RectTransform>();

            current.anchorMin = Vector2.one * 0.5f;
            current.anchorMax = Vector2.one * 0.5f;
            current.pivot = Vector2.one * 0.5f;

            current.sizeDelta = Vector2.one * 100;
        }

        public static void SetRelativeRectTransform() {
            RectTransform current = DragItem.transform.GetComponent<RectTransform>();

            current.anchorMin = Vector2.zero;
            current.anchorMax = Vector2.one;
            current.pivot = Vector2.one * 0.5f;

            current.sizeDelta = Vector2.zero;
            current.anchoredPosition3D = Vector3.zero;
        }

        public void SetCanvas(Transform canvas) {
            _canvas = canvas;
        }
    }
}
