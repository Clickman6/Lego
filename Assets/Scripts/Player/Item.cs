using Managers;
using UnityEngine;

namespace Player {
    public class Item : MonoBehaviour {
        public Vector3 Size;

        [SerializeField] private Material _allowMaterial;
        [SerializeField] private Material _denyMaterial;

        [SerializeField] private Renderer _renderer;

        private BoxCollider _collider;
        private BoxCollider _tmpCollider;

        private bool _inHand;
        private bool _canBuild;

        private void Awake() {
            _collider = GetComponent<BoxCollider>();
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
            SetMaterial(_allowMaterial);
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

            SetMaterial(_denyMaterial);
        }

        private void OnTriggerExit(Collider other) {
            if (!_inHand) return;
            _canBuild = true;

            SetMaterial(_allowMaterial);
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
