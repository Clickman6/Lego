using System;
using JetBrains.Annotations;
using Managers;
using UnityEngine;

namespace Player {
    public class Builder : MonoBehaviour {
        private Transform _cameraTransform;
        private Vector3 _rotation = Vector3.zero;
        private float _timer;
        [CanBeNull] private Item _item;

        [SerializeField] private float _gridStep;
        [SerializeField] private Transform _parent;

        private void Start() {
            _cameraTransform = GetComponent<Player>().CameraTransform;
        }

        private void Update() {
            if (GameManager.IsPause) return;
            if (!_item) return;

            _timer -= Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.G)) {
                ChangeGridSize();
            }

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
        }

        private void FixedUpdate() {
            if (Player.Inventory.GetItem() == null) return;

            if (Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out var hit,
                                PrivateSettings.RaycastDistance)) {
                if (!_item) {
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

            _item.transform.localEulerAngles = _rotation;
        }

        private void ChangeTransform(RaycastHit hit) {
            _item.transform.localPosition = GetBuildingPosition(hit);
        }

        private void AddingItem(RaycastHit hit) {
            _item = Instantiate(Player.Inventory.GetItem(), hit.point, Quaternion.Euler(_rotation))
                .GetComponent<Item>();
            _item.Adding();
        }

        private void BuildItem() {
            if (!_item.CanBuild()) {
                AudioManager.Instance.PlayDeny();
                return;
            }

            AudioManager.Instance.PlayBuild();

            _item.Build(_parent);

            _item = null;
        }

        public void RemoveItem() {
            if (!_item) return;

            Destroy(_item.gameObject);

            _item = null;
        }

        private Vector3 GetBuildingPosition(RaycastHit hit) {
            Vector3 point = hit.point;
            Vector3 normal = hit.normal;
            Vector3 offset = Vector3.zero;
            Vector3 position = hit.transform.position;

            offset.x = Mathf.Round(point.x * _gridStep) / _gridStep;
            offset.y = Mathf.Round(point.y * 2) / 2;
            offset.z = Mathf.Round(point.z * _gridStep) / _gridStep;

            if (!hit.transform.GetComponent<Item>()) return offset;

            bool isRotation = _rotation.y / 90 % 2 == 0;

            if (Math.Abs(Mathf.Abs(normal.x) - 1f) < 0.5f) {
                offset.x = point.x + Mathf.Sign(normal.x) * (isRotation ? _item.Size.x : _item.Size.z) / 2;
            }

            if (Math.Abs(Mathf.Abs(normal.y) - 1f) < 0.5f) {
                offset.y = position.y + _item.Size.y * Mathf.Sign(normal.y);
            } else {
                offset.y = position.y;
            }

            if (Math.Abs(Mathf.Abs(normal.z) - 1f) < 0.5f) {
                offset.z = point.z + Mathf.Sign(normal.z) * (isRotation ? _item.Size.z : _item.Size.x) / 2;
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
