using Scripts.Architecture.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    [RequireComponent(typeof(Slider))]

    public class ZombieHealthBar : MonoBehaviour
    {
        private readonly int _maximumSliderValue = 1;

        [SerializeField] private Slider _slider;

        private IZombieHealthService _zombieHealthService;

        private void OnDisable()
        {
            _zombieHealthService.HealthChanged -= OnHealthChanged;
        }

        private void Start()
        {
            _zombieHealthService = AllServices.Container.Single<IZombieHealthService>();
            _slider.value = _maximumSliderValue;

            _zombieHealthService.HealthChanged += OnHealthChanged;
        }

        private void OnHealthChanged(int value, int maximumValue)
        {
            _slider.value = (float)value / maximumValue;
        }
    }
}
