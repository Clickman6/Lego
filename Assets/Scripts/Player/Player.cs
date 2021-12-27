using System;
using Managers;
using UnityEngine;

namespace Player {
    public class Player : MonoBehaviour {
        public static Player Instance { get; private set; }

        public static Inventory Inventory { get; private set; }

        public Transform CameraTransform;
        
        private Vector3 _velocity;
        private float _angleX;
        private bool _isGrounded;
        private float _timer;
        private CharacterController _characterController;
        
        private float Sensitivity => _sensitivity * Settings.Instance.Sensitivity;

        [Header("Settings")]
        [SerializeField] private float _speed;
        [SerializeField] private float _jumpHeight;
        [SerializeField] private float _sensitivity;

        [Header("Ground")]
        [SerializeField] private Transform _grounded;
        [SerializeField] private LayerMask _layerMask;
        
        private void Awake() {
            Instance = this;
            Inventory = GetComponent<Inventory>();
        }

        private void Start() {
            _characterController = GetComponent<CharacterController>();

            CameraTransform = Camera.main.transform;

            _angleX = CameraTransform.rotation.x;
        }

        private void FixedUpdate() {
            CheckGround();
        }

        private void Update() {
            if (GameManager.IsPause) return;
            
            Move();
            RotationPlayer();
            RotationCamera();
        }

        private void RotationCamera() {
            float v = Input.GetAxis("Mouse Y");

            _angleX -= v * Sensitivity;

            _angleX = Mathf.Clamp(_angleX, -80, 80);

            CameraTransform.localRotation = Quaternion.Euler(Vector3.right * _angleX);
        }

        private void RotationPlayer() {
            float h = Input.GetAxis("Mouse X");

            transform.Rotate(Vector3.up * h * Sensitivity);
        }

        private void Move() {
            float speedMultiple = 1f;
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            Vector3 movement = transform.forward * v + transform.right * h;

            if (_isGrounded) {
                _velocity.y = -1f;
                
                Jump();
            } else {
                _velocity.y += Physics.gravity.y * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.LeftShift)) {
                speedMultiple = 1.5f;
            } else if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.C)) {
                speedMultiple = 0.35f;
            }

            _characterController.Move((movement * _speed * speedMultiple + _velocity) * Time.deltaTime);
        }

        private void Jump() {
            if (!Input.GetKey(KeyCode.Space)) return;

            _timer = 0.1f;
            _isGrounded = false;

            _velocity.y = Mathf.Sqrt(_jumpHeight * 2 * -Physics.gravity.y);
        }

        private void CheckGround() {
            _timer -= Time.fixedDeltaTime;
            
            if (_timer <= 0f) {
                _isGrounded = Physics.CheckSphere(_grounded.position, 0.15f, _layerMask);
            }
        }

        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_grounded.position, 0.15f);
        }
    }
}
