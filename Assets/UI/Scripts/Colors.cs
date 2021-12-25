using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Scripts {
    public class Colors : MonoBehaviour {
        [SerializeField] private List<Material> _materials = new List<Material>();
        [SerializeField] private GameObject _template;
        [SerializeField] private Image _image;
        [SerializeField] private GameObject _list;

        private void Start() {
            Init();
        }

        public void Init() {
            for (int i = 0; i < _template.transform.parent.childCount; i++) {
                Destroy(_template.transform.parent.GetChild(i).gameObject);
            }

            for (int i = 0; i < _materials.Count; i++) {
                int b = i;

                GameObject tmp = Instantiate(_template, _template.transform.parent);

                Image image = tmp.GetComponent<Image>();
                Button button = tmp.GetComponent<Button>();

                image.enabled = true;
                image.color = _materials[i].color;

                button.enabled = true;
                button.onClick.AddListener(() => Select(b));

                tmp.SetActive(true);
            }

            Select(0);
        }

        private void SetMaterial(Material material) {
            Player.Player.Inventory.CurrentMaterial = material;
        }

        private void Select(int index) {
            _image.color = _materials[index].color;

            SetMaterial(_materials[index]);
            Player.Inventory.Painter.UnSelect();
            Hide();
        }

        public void Show() {
            _list.SetActive(true);
        }

        public void Hide() {
            _list.SetActive(false);
        }

    }
}
