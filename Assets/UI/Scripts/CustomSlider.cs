using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI {
    [ExecuteAlways]
    [RequireComponent(typeof(Slider))]
    public class CustomSlider : MonoBehaviour {
        private Slider _slider;

        [SerializeField] public TextMeshProUGUI _valueLabel;
        [SerializeField] private string _format;

        private void Awake() {
            _slider = GetComponent<Slider>();
            _slider.onValueChanged.AddListener(SetValueLabel);
        }

        public Slider.SliderEvent onValueChanged => _slider.onValueChanged;

        public void SetValue(float value) {
            _slider.value = value;
            
            SetValueLabel(_slider.value);
        }

        private void SetValueLabel(float value) {
            _valueLabel.text = value.ToString(_format);
        }
    }
}
