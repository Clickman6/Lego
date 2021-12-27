using System;
using IJunior.TypedScenes;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Managers {
    public class PlaygroundEntry : MonoBehaviour, ISceneLoadHandler<bool> {
        private bool _load;

        [SerializeField] private GameObject _continue;
        [SerializeField] private GameObject _button;
        [SerializeField] private GameObject _loading;
        [SerializeField] private Image _loadingProgressBar;
        [SerializeField] private TextMeshProUGUI _loadingPercentage;
        
        public void OnSceneLoaded(bool load) {
            _load = load;
        }

        public void Start() {
            // GameManager.Stop();

            _continue.SetActive(true);

            if (_load) {
                Load();
            } else {
                ShowButton();
            }
        }

        public void Continue() {
            _continue.SetActive(false);
            GameManager.Play();

            Destroy(gameObject);
        }

        private void ShowButton() {
            _loading.SetActive(false);
            _button.SetActive(true);
        }

        private void Load() {
            int amount = 100000;
            int progress;

            for (int i = 0; i < amount; i++) {
                progress = Mathf.RoundToInt(i * 100 / amount);

                _loadingPercentage.text = progress + "%";

                _loadingProgressBar.fillAmount =
                    Mathf.Lerp(_loadingProgressBar.fillAmount, progress, Time.deltaTime * 5);
            }

            ShowButton();
        }
    }
}
