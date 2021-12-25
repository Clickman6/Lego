using System;
using TMPro;
using UI.Scripts;
using UnityEngine;

namespace UI {
    public class UIManager : MonoBehaviour {
        public static UIManager Instance { get; private set; }

        public Inventory Inventory;
        public Colors Colors;
        [SerializeField] private TextMeshProUGUI _inventoryText;

        private void Awake() {
            Instance = this;
            Inventory.Init();
            Colors.Init();
        }

        public void SetInventoryText(String text) {
            _inventoryText.text = text;
        }

        public void ShowInventory() {
            Inventory.Show();
        }
    
        public void HideInventory() {
            Inventory.Hide();
        }
    }
}
