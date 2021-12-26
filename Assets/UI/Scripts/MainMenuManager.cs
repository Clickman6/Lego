using IJunior.TypedScenes;
using UnityEngine;

namespace UI.Scripts {
    public class MainMenuManager : MonoBehaviour {
        public void OnNewGameButton() {
            SceneTransition.SwitchToScene(() => Playground.LoadAsync(false));
        }

        public void OnExitButton() {
            Application.Quit();
        }
        
        public void OnLoadButton() {
            SceneTransition.SwitchToScene(() => Playground.LoadAsync(true));
        }
    }
}
