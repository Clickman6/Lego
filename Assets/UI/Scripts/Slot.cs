using System;
using Managers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Scripts {
    public class Slot : MonoBehaviour, IDropHandler {
        private GameObject _itemUI;
        
        [SerializeField] private GameObject Checked;

        public void OnDrop(PointerEventData eventData) {
            Destroy(_itemUI);

            _itemUI = DragHandler.DragItem;

            _itemUI.transform.SetParent(transform);
            DragHandler.SetRelativeRectTransform();
            
            GameManager.Instance.ActiveBuilder();
        }

        public GameObject GetPrefab() {
            if (_itemUI == null) return null;

            return _itemUI.GetComponent<ItemSprite>().Prefab;
        }

        public String GetText() {
            return !_itemUI ? "Не выбран" : _itemUI.GetComponent<ItemSprite>().Text;

        }
        
        public void Check() {
            Checked.SetActive(true);
        }

        public void UnCheck() {
            Checked.SetActive(false);
        }
    }
}
