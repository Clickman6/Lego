using JetBrains.Annotations;
using Managers;
using UnityEngine;

namespace Player {
    public class Painter : MonoBehaviour {
        private Transform _cameraTransform;
        private float _timer;
        private Material _lastMaterial;
        [CanBeNull] private Item _selectedItem;

        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private GameObject _checkedUI;

        private void Start() {
            _cameraTransform = GetComponent<Player>().CameraTransform;
        }

        private void Update() {
            if (GameManager.IsPause) return;
            if (!_selectedItem) return;

            _timer += Time.deltaTime;

            if (Input.GetKey(KeyCode.Mouse0) && _timer > 0.3f) {
                _timer = 0f;
                Paint();
            }
            
            if (Input.GetKeyUp(KeyCode.Mouse0)) {
                _timer = 0f;
            }
        }

        private void FixedUpdate() {
            if (Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out var hit,
                                PrivateSettings.RaycastDistance, _layerMask)) {
                Select(hit.transform.GetComponent<Item>());
            } else if (_selectedItem) {
                UnSelect();
            }
        }

        private void Select(Item item) {
            if (_selectedItem?.GetInstanceID() == item.GetInstanceID()) return;

            UnSelect();

            _lastMaterial = item.GetMaterial();
            _selectedItem = item;
            SetMaterial(Player.Inventory.CurrentMaterial);
        }

        public void UnSelect() {
            SetMaterial(_lastMaterial);
            _selectedItem = null;
        }

        private void Paint() {
            AudioManager.Instance.PlaySuccess();

            SetMaterial(Player.Inventory.CurrentMaterial);
            _selectedItem = null;
        }

        private void SetMaterial(Material material) {
            _selectedItem?.SetMaterial(material);
        }

        public void Enable() {
            _checkedUI.SetActive(true);
            enabled = true;
        }

        public void Disable() {
            UnSelect();

            _checkedUI.SetActive(false);
            enabled = false;
        }
    }
}
