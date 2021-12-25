using IJunior.TypedScenes;
using TMPro;
using UI.Scripts;
using UnityEngine;

namespace Managers {
    public class PlaygroundEntry : MonoBehaviour, ISceneLoadHandler<int> {
        [SerializeField] private GameObject _continue;
        [SerializeField] private GameObject _button;
        [SerializeField] private TextMeshProUGUI _text;

        public void OnSceneLoaded(int loadWorld) {
            Invoke("LateLoad", 0.1f);
        }

        public void LateLoad() {
            GameManager.Instance.Stop();
            
            _continue.SetActive(true);
            _button.SetActive(true);
            
            _text.text = "";
        }

        public void Continue() {
            _continue.SetActive(false);
            GameManager.Instance.Play();
        }
    }
}
