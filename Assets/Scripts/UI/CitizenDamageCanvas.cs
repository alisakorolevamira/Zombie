using Scripts.Characters.Citizens;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class CitizenDamageCanvas : MonoBehaviour
    {
        private readonly float _textFadeDuration = 3f;
        private readonly int _visibleAlfaIndex = 1;
        private readonly int _invisibleAlfaIndex = 0;
        private readonly float _lerpStep = 0.15f;
        private readonly float _coefficientForHealth = 100f;

        [SerializeField] private Citizen _citizen;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Image _image;
        [SerializeField] private Color _greenColor;

        private float _lerpIndex = 0;

        private void OnEnable()
        {
            _citizen.HealthChanged += ChangeText;
        }

        private void OnDisable()
        {
            _citizen.HealthChanged -= ChangeText;
        }

        private void ChangeText(int damage)
        {
            ChangeTextColor();

            _text.canvasRenderer.SetAlpha(_visibleAlfaIndex);
            _text.text = damage.ToString();
            _text.CrossFadeAlpha(_invisibleAlfaIndex, _textFadeDuration, false);
            _image.fillAmount = _citizen.Health / _coefficientForHealth;
        }

        private void ChangeTextColor()
        {
            _lerpIndex += _lerpStep;

            _text.color = Color.Lerp(_greenColor, Color.red, _lerpIndex);
            _image.color = Color.Lerp(_greenColor, Color.red, _lerpIndex);
        }
    }
}
