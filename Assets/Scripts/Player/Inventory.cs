using System.Collections.Generic;
using Managers;
using UI;
using UnityEngine;

namespace Player {
    public class Inventory : MonoBehaviour {
        public static Builder Builder { get; private set; }

        public static Trash Trash { get; private set; }

        public static Painter Painter { get; private set; }

        public Material CurrentMaterial {
            get => _currentMaterial;
            set => _currentMaterial = value;
        }

        private float _currentIndex;
        private Material _currentMaterial;

        [SerializeField] private List<Slot> _items = new List<Slot>();

        private void Awake() {
            Builder = GetComponent<Builder>();
            Trash = GetComponent<Trash>();
            Painter = GetComponent<Painter>();
        }

        private void Update() {
            if (GameManager.IsPause) return;

            ChangeSlotByKey();
            ChangeSlotByScroll();
            ChangeToTrashByKey();
            ChangeToPainterByKey();
        }

        //Item
        public GameObject GetItem() {
            return _items[GetCurrentIndex()].GetPrefab();
        }

        private int GetCurrentIndex() {
            return Mathf.Abs(Mathf.FloorToInt(_currentIndex * 10) - Mathf.FloorToInt(_currentIndex)) % _items.Count;
        }

        //Activators
        public void SetBuilder() {
            DisableTools();
            EnableCurrentSlot();

            Builder.Enable();
        }

        public void SetTrash() {
            DisableAllSlot();
            DisableTools();

            Trash.Enable();

            UI.Inventory.Instance.SetText("Инструмент удаления");
        }

        public void SetPainter() {
            DisableAllSlot();
            DisableTools();

            Painter.Enable();

            UI.Inventory.Instance.SetText("Инструмент кисть");
        }

        // Slots
        private void EnableCurrentSlot() {
            DisableAllSlot();

            _items[GetCurrentIndex()].Check();
            UI.Inventory.Instance.SetText($"Инструмент строитель ({_items[GetCurrentIndex()].GetText()})");
        }

        private void DisableAllSlot() {
            foreach (var item in _items) {
                item.UnCheck();
            }

            Builder.RemoveItem();
        }

        private void DisableTools() {
            Builder.Disable();
            Trash.Disable();
            Painter.Disable();
        }

        // Inputs
        private void ChangeSlotByScroll() {
            if (Input.mouseScrollDelta.y == 0) return;

            _currentIndex += Input.mouseScrollDelta.y;

            SetBuilder();
        }

        private void ChangeSlotByKey() {
            bool keyDown = false;

            if (Input.GetKeyDown(KeyCode.Alpha1)) {
                _currentIndex = 0;
                keyDown = true;
            } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
                _currentIndex = 1f;
                keyDown = true;
            } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
                _currentIndex = 2;
                keyDown = true;
            } else if (Input.GetKeyDown(KeyCode.Alpha4)) {
                _currentIndex = 3;
                keyDown = true;
            } else if (Input.GetKeyDown(KeyCode.Alpha5)) {
                _currentIndex = 4;
                keyDown = true;
            }

            if (!keyDown) return;

            _currentIndex /= 10f;

            SetBuilder();
        }

        private void ChangeToTrashByKey() {
            if (Input.GetKeyDown(KeyCode.T)) {
                SetTrash();
            }
        }

        private void ChangeToPainterByKey() {
            if (Input.GetKeyDown(KeyCode.B)) {
                SetPainter();
            }
        }
    }
}
