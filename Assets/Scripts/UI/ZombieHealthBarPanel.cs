using Scripts.Architecture.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    [RequireComponent(typeof(Slider))]

    public class ZombieHealthBarPanel : Panel
    {
        private readonly int _maximumSliderValue = 1;
        private IZombieHealthService _zombieHealthService;
        private Slider _slider;

        private void OnEnable()
        {
            _zombieHealthService = AllServices.Container.Single<IZombieHealthService>();
            _zombieHealthService.HealthChanged += OnHealthChanged;
        }

        private void OnDisable()
        {
            _zombieHealthService.HealthChanged -= OnHealthChanged;
        }

        private void Start()
        {
            _slider = GetComponent<Slider>();
            _slider.value = _maximumSliderValue;

            Open();
        }

        private void OnHealthChanged(int value, int maximumValue)
        {
            _slider.value = (float)value / maximumValue;
        }
    }
}
