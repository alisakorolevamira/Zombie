using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SitizenDamageText : MonoBehaviour
{
    [SerializeField] private Sitizen _sitizen;
    [SerializeField] private TMP_Text _text;
    private float _textFadeDuration = 3f;

    private void OnEnable()
    {
        _sitizen.HealthChanged += ChangeText;
    }

    private void OnDisable()
    {
        _sitizen.HealthChanged -= ChangeText;
    }

    private void ChangeText(int damage)
    {
        _text.canvasRenderer.SetAlpha(1);
        _text.text = damage.ToString();
        _text.CrossFadeAlpha(0, _textFadeDuration, false);
    }
}
