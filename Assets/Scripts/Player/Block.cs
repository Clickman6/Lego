using System;
using Managers;
using UnityEngine;

namespace Player {
    public class Block : MonoBehaviour {
        public int Id;
        [NonSerialized] public Vector3 Size;

        private BoxCollider _collider;
        private BoxCollider _tmpCollider;

        private bool _inHand;
        private bool _canBuild;

        [SerializeField] private Renderer _renderer;

        private void Awake() {
            _collider = GetComponent<BoxCollider>();
            Size = _collider.size;
        }

        public void Build(Transform parent) {
            _inHand = false;

            gameObject.layer = LayerMask.NameToLayer("Building");
            transform.parent = parent;

            SetMaterial(Player.Inventory.CurrentMaterial);
            DisableRigidbody();
            DisableTriggerCollider();
        }

        public void Adding() {
            _inHand = true;
            _canBuild = true;

            gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");

            EnableRigidbody();
            EnableTriggerCollider();
            SetMaterial(Player.Inventory.Builder.AllowMaterial);
        }

        public void SetMaterial(Material material) {
            _renderer.material = material;
        }

        private void DisableRigidbody() {
            Destroy(gameObject.GetComponent<Rigidbody>());
        }

        private void EnableRigidbody() {
            Rigidbody rb = gameObject.AddComponent<Rigidbody>();

            rb.isKinematic = true;
            rb.useGravity = false;
        }

        private void DisableTriggerCollider() {
            Destroy(_tmpCollider);
            _collider.enabled = true;
        }

        private void EnableTriggerCollider() {
            _collider.enabled = false;

            _tmpCollider = gameObject.AddComponent<BoxCollider>();

            _tmpCollider.isTrigger = true;
            _tmpCollider.center = _collider.center;
            _tmpCollider.size = _collider.size - (Vector3.one * 0.01f);
        }

        public bool CanBuild() {
            return _canBuild;
        }

        private void OnTriggerStay(Collider other) {
            if (!_inHand) return;
            _canBuild = false;

            SetMaterial(Player.Inventory.Builder.DenyMaterial);
        }

        private void OnTriggerExit(Collider other) {
            if (!_inHand) return;
            _canBuild = true;

            SetMaterial(Player.Inventory.Builder.AllowMaterial);
        }

        public void Die() {
            AudioManager.Instance.PlayDestroy();

            Destroy(gameObject);
        }

        public Material GetMaterial() {
            return _renderer.sharedMaterial;
        }
    }
}
