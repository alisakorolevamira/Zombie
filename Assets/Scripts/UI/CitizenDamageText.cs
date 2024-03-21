using Scripts.Characters.Citizens;
using TMPro;
using UnityEngine;

namespace Scripts.UI
{
    public class CitizenDamageText : MonoBehaviour
    {
        private readonly float _textFadeDuration = 3f;
        private readonly int _visibleAlfaIndex = 1;
        private readonly int _invisibleAlfaIndex = 0;
        private readonly float _lerpStep = 0.15f;

        [SerializeField] private Citizen _citizen;
        [SerializeField] private TMP_Text _text;

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
        }

        private void ChangeTextColor()
        {
            _lerpIndex += _lerpStep;
            _text.color = Color.Lerp(Color.green, Color.red, _lerpIndex);
        }
    }
}
