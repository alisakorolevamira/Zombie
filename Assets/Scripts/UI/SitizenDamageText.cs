using Scripts.Characters.Sitizens;
using TMPro;
using UnityEngine;

namespace Scripts.UI
{
    public class SitizenDamageText : MonoBehaviour
    {
        private readonly float _textFadeDuration = 3f;

        [SerializeField] private Sitizen _sitizen;
        [SerializeField] private TMP_Text _text;

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
}
