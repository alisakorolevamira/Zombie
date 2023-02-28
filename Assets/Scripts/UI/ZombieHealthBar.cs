using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieHealthBar : MonoBehaviour
{
    [SerializeField] private Zombie _zombie;
    [SerializeField] private Slider _slider;

    private void OnEnable()
    {
        _zombie.HealthChanged += OnHealthChanged;
        _slider.value = 1;
    }

    private void OnDisable()
    {
        _zombie.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(int value, int maximumValue)
    {
        _slider.value = (float)value / maximumValue;
    }
}
