using System;
using JetBrains.Annotations;
using Managers;
using UnityEngine;

namespace Player {
    public class Builder : MonoBehaviour {
        public Material AllowMaterial;
        public Material DenyMaterial;
        
        private Transform _cameraTransform;
        private Vector3 _rotation = Vector3.zero;
        private float _timer;
        [CanBeNull] private Block _block;

        [SerializeField] private float _gridStep;
        [SerializeField] private Transform _parent;

        private void Start() {
            _cameraTransform = GetComponent<Player>().CameraTransform;
        }

        private void Update() {
            if (GameManager.IsPause) return;
            if (!_block) return;

            _timer -= Time.deltaTime;
            
            if (Input.GetKey(KeyCode.Mouse0) && _timer <= 0f) {
                ResetTimer();
                BuildItem();
            }

            if (Input.GetKeyUp(KeyCode.Mouse0)) {
                ResetTimer();
            }

            if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.R)) {
                Rotation();
            }

            if (Input.GetKeyDown(KeyCode.G)) {
                ChangeGridSize();
            }
        }

        private void FixedUpdate() {
            if (Player.Inventory.GetItem() == null) return;

            if (Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out var hit,
                                Settings.RaycastDistance)) {
                if (!_block) {
                    AddingItem(hit);
                } else {
                    ChangeTransform(hit);
                }
            } else {
                RemoveItem();
            }

        }

        private void Rotation(int coefficient = 1) {
            _rotation.y += 90 * coefficient;

            if (_rotation.y >= 360) {
                _rotation = Vector3.zero;
            }

            _block.transform.localEulerAngles = _rotation;
        }

        private void ChangeTransform(RaycastHit hit) {
            _block.transform.localPosition = GetBuildingPosition(hit);
        }

        private void AddingItem(RaycastHit hit) {
            _block = Instantiate(Player.Inventory.GetItem(), hit.point, Quaternion.Euler(_rotation))
                .GetComponent<Block>();
            _block.Adding();
        }

        private void BuildItem() {
            if (!_block.CanBuild()) {
                AudioManager.Instance.PlayDeny();
                return;
            }

            AudioManager.Instance.PlayBuild();

            _block.Build(_parent);

            _block = null;
        }

        public void RemoveItem() {
            if (!_block) return;

            Destroy(_block.gameObject);

            _block = null;
        }

        private Vector3 GetBuildingPosition(RaycastHit hit) {
            Vector3 point = hit.point;
            Vector3 normal = hit.normal;
            Vector3 offset = Vector3.zero;
            Vector3 position = hit.transform.position;

            offset.x = Mathf.Round(point.x * _gridStep) / _gridStep;
            offset.y = Mathf.Round(point.y * 2) / 2;
            offset.z = Mathf.Round(point.z * _gridStep) / _gridStep;

            if (!hit.transform.GetComponent<Block>()) return offset;

            bool isRotation = _rotation.y / 90 % 2 == 0;

            if (Math.Abs(Mathf.Abs(normal.x) - 1f) < 0.5f) {
                offset.x = point.x + Mathf.Sign(normal.x) * (isRotation ? _block.Size.x : _block.Size.z) / 2;
            }

            if (Math.Abs(Mathf.Abs(normal.y) - 1f) < 0.5f) {
                offset.y = position.y + _block.Size.y * Mathf.Sign(normal.y);
            } else {
                offset.y = position.y;
            }

            if (Math.Abs(Mathf.Abs(normal.z) - 1f) < 0.5f) {
                offset.z = point.z + Mathf.Sign(normal.z) * (isRotation ? _block.Size.z : _block.Size.x) / 2;
            }

            return offset;
        }

        private void ResetTimer() {
            _timer = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.C) ? 0.575f : 0.25f;
        }

        private void ChangeGridSize() {
            _gridStep /= 2;

            if (_gridStep < 1f) {
                _gridStep = 4f;
            }
        }

        public void Enable() {
            enabled = true;
        }

        public void Disable() {
            enabled = false;
        }
    }
}
