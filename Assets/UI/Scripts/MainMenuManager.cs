using IJunior.TypedScenes;
using UnityEngine;

namespace UI.Scripts {
    public class MainMenuManager : MonoBehaviour {
        public void StartNewGame() {
            SceneTransition.SwitchToScene(() => Playground.LoadAsync(333));
        }
    }
}
