using JetBrains.Annotations;
using Managers;
using UnityEngine;

namespace Player {
    public class Painter : MonoBehaviour {
        private Transform _cameraTransform;
        private float _timer;
        private Material _lastMaterial;
        [CanBeNull] private Block _selectedBlock;

        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private GameObject _checkedUI;

        private void Start() {
            _cameraTransform = GetComponent<Player>().CameraTransform;
        }

        private void Update() {
            if (GameManager.IsPause) return;
            if (!_selectedBlock) return;

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
                                Settings.RaycastDistance, _layerMask)) {
                Select(hit.transform.GetComponent<Block>());
            } else if (_selectedBlock) {
                UnSelect();
            }
        }

        private void Select(Block block) {
            if (_selectedBlock?.GetInstanceID() == block.GetInstanceID()) return;

            UnSelect();

            _lastMaterial = block.GetMaterial();
            _selectedBlock = block;
            SetMaterial(Player.Inventory.CurrentMaterial);
        }

        public void UnSelect() {
            SetMaterial(_lastMaterial);
            _selectedBlock = null;
        }

        private void Paint() {
            AudioManager.Instance.PlaySuccess();

            SetMaterial(Player.Inventory.CurrentMaterial);
            _selectedBlock = null;
        }

        private void SetMaterial(Material material) {
            _selectedBlock?.SetMaterial(material);
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
