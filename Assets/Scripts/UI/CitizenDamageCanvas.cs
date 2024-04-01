using Scripts.Characters.Citizens;
using Scripts.Constants;
using TMPro;
using UnityEngine;

namespace Scripts.UI
{
    public class CitizenDamageCanvas : MonoBehaviour
    {
        [SerializeField] private Citizen _citizen;
        [SerializeField] private TMP_Text _text;
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

            _text.canvasRenderer.SetAlpha(UIConstants.VisibleAlfaIndex);
            _text.text = damage.ToString();
            _text.CrossFadeAlpha(UIConstants.InvisibleAlfaIndex, UIConstants.TextFadeDuration, false);
        }

        private void ChangeTextColor()
        {
            _lerpIndex += UIConstants.LerpStep;

            _text.color = Color.Lerp(_greenColor, Color.red, _lerpIndex);
        }
    }
}