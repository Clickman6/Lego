using System;
using UnityEngine;

namespace UI {
    public class Settings : MonoBehaviour {
        [SerializeField] private CustomSlider _volume;
        [SerializeField] private CustomSlider _sensitivity;

        private void Start() {
            InitVolume();
            InitSensitivity();
        }

        private void InitVolume() {
            _volume.SetValue(global::Settings.Instance.Volume);
            _volume.onValueChanged.AddListener(ChangeVolume);
        }

        private void InitSensitivity() {
            _sensitivity.SetValue(global::Settings.Instance.Sensitivity);
            _sensitivity.onValueChanged.AddListener(ChangeSensitivity);
        }

        private static void ChangeVolume(float value) {
            global::Settings.Instance.Volume = value;
        }

        private static void ChangeSensitivity(float value) {
            global::Settings.Instance.Sensitivity = value;
        }

        public void Save() {
            global::Settings.Instance.Save();
        }
    }
}
