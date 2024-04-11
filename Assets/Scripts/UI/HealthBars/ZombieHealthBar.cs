using Architecture.Services;
using Architecture.ServicesInterfaces.Zombie;
using Constants.Characters;
using Constants.UI;
using UnityEngine;
using UnityEngine.UI;

namespace UI.HealthBars
{
    [RequireComponent(typeof(Slider))]

    public class ZombieHealthBar : MonoBehaviour
    {
        [SerializeField] private Slider _slider;

        private IZombieHealthService _zombieHealthService;

        private void OnDisable()
        {
            _zombieHealthService.HealthChanged -= OnHealthChanged;
        }

        private void Start()
        {
            _zombieHealthService = AllServices.Container.Single<IZombieHealthService>();
            _slider.value = UIConstants.MaximumSliderValue;

            _zombieHealthService.HealthChanged += OnHealthChanged;
        }

        private void OnHealthChanged(int value)
        {
            _slider.value = (float)value / ZombieConstants.ZombieMaximumHealth;
        }
    }
}