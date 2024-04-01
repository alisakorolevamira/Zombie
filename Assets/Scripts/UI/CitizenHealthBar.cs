using Scripts.Characters.Citizens;
using Scripts.Constants;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class CitizenHealthBar : MonoBehaviour
    {
        [SerializeField] private Citizen _citizen;
        [SerializeField] private Slider _slider;
        [SerializeField] private Image _sliderImage;
        [SerializeField] private Color _greenColor;

        private float _lerpIndex = 0;
        private float _maximumHealth;
        private float _currentHealth;

        private void OnEnable()
        {
            _citizen.HealthChanged += OnHealthChanged;
        }

        private void OnDisable()
        {
            _citizen.HealthChanged -= OnHealthChanged;
        }

        private void Start()
        {
            _maximumHealth = _citizen.Health;
            _currentHealth = _maximumHealth;
            _slider.value = UIConstants.MaximumSliderValue;
        }

        private void OnHealthChanged(int value)
        {
            _currentHealth += value;
            _lerpIndex += UIConstants.LerpStep;

            _slider.value = _currentHealth / _maximumHealth;
            _sliderImage.color = Color.Lerp(_greenColor, Color.red, _lerpIndex);
        }
    }
}