using System;
using Entries;
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
            PlaygroundEntry.Instance.Save();
            
            GameManager.UnPause();
            GameManager.ShowCursor();

            Transition.SwitchToScene(() => IJunior.TypedScenes.Menu.LoadAsync());
        }
        
        public void OnExitButton() {
            PlaygroundEntry.Instance.Save();

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
