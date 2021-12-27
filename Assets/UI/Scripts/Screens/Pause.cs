using System;
using Managers;
using UnityEngine;

namespace UI {
    public class Pause : MonoBehaviour {
        public static Pause Instance { get; private set; }

        private void Awake() {
            Instance = this;
            Hide();
        }

        public void OnCloseButton() {
            GameManager.ClosePauseMenu();
        }
        
        public void OnMenuButton() {
            GameManager.UnPause();
            GameManager.ShowCursor();
            
            Transition.SwitchToScene(() => IJunior.TypedScenes.Menu.LoadAsync());
        }
        
        public void OnExitButton() {
            Application.Quit();
        }

        public void Show() {
            gameObject.SetActive(true);
        }

        public void Hide() {
            gameObject.SetActive(false);
        }
    }
}
