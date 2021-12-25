using System.Collections.Generic;
using UnityEngine;

namespace UI.Scripts {
    public class Inventory : MonoBehaviour {
        [SerializeField] private GameObject _cellPrefab;

        [SerializeField] private RectTransform _contentView;
        [SerializeField] private List<GameObject> _items = new List<GameObject>();

        public void Init() {
            for (int i = 0; i < _contentView.childCount; i++) {
                Destroy(_contentView.GetChild(i).gameObject);
            }

            foreach (var item in _items) {
                var cell = Instantiate(_cellPrefab, _contentView); 
                
                Instantiate(item, cell.transform).GetComponent<DragHandler>().SetCanvas(transform.parent);
            }
        }

        public void Show() {
            gameObject.SetActive(true);
        }

        public void Hide() {
            gameObject.SetActive(false);
        }
    }
}
