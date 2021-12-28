using System;
using IJunior.TypedScenes;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class Menu : MonoBehaviour {
        [SerializeField] private Button _loadButton;

        private void Start() {
            _loadButton.interactable = SaveLoad.SaveLoad.HasFile();
        }

        public void OnNewGameButton() {
            Transition.SwitchToScene(() => Playground.LoadAsync(false));
        }

        public void OnLoadButton() {
            Transition.SwitchToScene(() => Playground.LoadAsync(true));
        }

        public void OnExitButton() {
            Application.Quit();
        }
    }
}
