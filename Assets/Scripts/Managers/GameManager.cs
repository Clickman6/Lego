using System.Collections;
using UI;
using UI.Scripts;
using UnityEngine;

namespace Managers {
    public class GameManager : MonoBehaviour {
        public static GameManager Instance { get; private set; }

        public static bool IsPlay = true;
        public static bool IsPause;

        private void Awake() {
            Instance = this;
            HideCursor();
        }

        private void Update() {
            if(!IsPlay) return;
            
            if (IsPause) {
                if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E)) {
                    CloseInventory();
                }
            } else {
                if (Input.GetKeyDown(KeyCode.E)) {
                    OpenInventory();
                }
            }
        }

        //Inventory
        public void OpenInventory() {
            UIManager.Instance.ShowInventory();
            Pause();
        }

        public void CloseInventory() {
            UIManager.Instance.HideInventory();
            UnPause();
        }

        public void ActiveBuilder() {
            Player.Player.Inventory.SetBuilder();
        }

        public void ActiveTrash() {
            Player.Player.Inventory.SetTrash();
        }

        public void ActivePainter() {
            Player.Player.Inventory.SetPainter();
        }

        //Other
        public void ShowCursor() {
            Cursor.lockState = CursorLockMode.Confined;
        }

        public void HideCursor() {
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void Pause() {
            ShowCursor();
            IsPause = true;
            Time.timeScale = 0;
        }
        
        public void Play() {
            IsPlay = true;
            UnPause();
        }
        
        public void Stop() {
            IsPlay = false;
            Pause();
        }

        public void UnPause() {
            HideCursor();
            IsPause = false;
            Time.timeScale = 1;
        }
    }
}
