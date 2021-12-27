using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI {
    public class Inventory : MonoBehaviour {
        public static Inventory Instance { get; private set; }

        [SerializeField] private GameObject _cellPrefab;

        [SerializeField] private RectTransform _contentView;
        [SerializeField] private List<GameObject> _items = new List<GameObject>();
        [SerializeField] private TextMeshProUGUI _label;

        private void Awake() {
            Instance = this;

            Init();
            gameObject.SetActive(false);
        }

        public void Init() {
            for (int i = 0; i < _contentView.childCount; i++) {
                Destroy(_contentView.GetChild(i).gameObject);
            }

            foreach (var item in _items) {
                var cell = Instantiate(_cellPrefab, _contentView); 
                
                Instantiate(item, cell.transform).GetComponent<DragHandler>().SetCanvas(transform.parent);
            }
        }

        public void SetText(String text) {
            _label.text = text;
        }
        
        public void Show() {
            gameObject.SetActive(true);
        }

        public void Hide() {
            gameObject.SetActive(false);
        }
    }
}
