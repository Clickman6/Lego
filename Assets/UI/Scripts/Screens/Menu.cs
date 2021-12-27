using IJunior.TypedScenes;
using UnityEngine;

namespace UI {
    public class Menu : MonoBehaviour {
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
