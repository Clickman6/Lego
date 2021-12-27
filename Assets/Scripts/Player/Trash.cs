using System;
using JetBrains.Annotations;
using Managers;
using UnityEngine;

namespace Player {
    public class Trash : MonoBehaviour {
        private Transform _cameraTransform;
        private float _timer;
        private Material _lastMaterial;
        [CanBeNull] private Item _selectedItem;

        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private Material _selectedMaterial;
        [SerializeField] private GameObject _checkedUI;

        private void Start() {
            _cameraTransform = GetComponent<Player>().CameraTransform;
        }

        private void Update() {
            if (GameManager.IsPause) return;
            if (!_selectedItem) return;

            _timer += Time.deltaTime;

            if (Input.GetKey(KeyCode.Mouse0) && _timer > 0.25f) {
                _timer = 0f;
                Remove();
            }

            if (Input.GetKeyUp(KeyCode.Mouse0)) {
                _timer = 0f;
            }
        }

        private void FixedUpdate() {
            if (Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out var hit,
                                Settings.RaycastDistance, _layerMask)) {
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
            SetMaterial(_selectedMaterial);
        }

        private void UnSelect() {
            SetMaterial(_lastMaterial);
            _selectedItem = null;
        }

        private void Remove() {
            _selectedItem.Die();
            UnSelect();
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
