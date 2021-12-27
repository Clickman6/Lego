using UI;
using UnityEngine;

namespace Managers {
    public class GameManager : MonoBehaviour {
        public static GameManager Instance { get; private set; }

        public static bool IsPause;
        private static bool IsPlay = true;
        
        private void Awake() {
            Instance = this;
        }

        private void Update() {
            if(!IsPlay) return;

            if (IsPause) {
                if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E)) {
                    CloseInventory();
                    ClosePauseMenu();
                }
            } else {
                if (Input.GetKeyDown(KeyCode.Escape)) {
                    OpenPauseMenu();
                }
                
                if (Input.GetKeyDown(KeyCode.E)) {
                    OpenInventory();
                }
            }
        }
        
        // Pause Menu
        private static void OpenPauseMenu() {
            UI.Pause.Instance.Show();
            Pause();
        }
        
        public static void ClosePauseMenu() {
            UI.Pause.Instance.Hide();
            
            UnPause();
        }
        
        // Inventory
        private static void OpenInventory() {
            Inventory.Instance.Show();
            
            Pause();
        }

        private static void CloseInventory() {
            Inventory.Instance.Hide();

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
        public static void ShowCursor() {
            Cursor.lockState = CursorLockMode.Confined;
        }

        public static void HideCursor() {
            Cursor.lockState = CursorLockMode.Locked;
        }

        public static void Pause() {
            ShowCursor();
            IsPause = true;
            Time.timeScale = 0;
        }
        
        public static void Play() {
            IsPlay = true;
            UnPause();
        }
        
        public static void Stop() {
            IsPlay = false;
            Pause();
        }

        public static void UnPause() {
            HideCursor();
            IsPause = false;
            Time.timeScale = 1;
        }
    }
}
