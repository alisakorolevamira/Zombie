using Scripts.Characters.Sitizens;
using TMPro;
using UnityEngine;

namespace Scripts.UI
{
    public class SitizenDamageText : MonoBehaviour
    {
        private readonly float _textFadeDuration = 3f;
        private readonly int _visibleAlfaIndex = 1;
        private readonly int _invisibleAlfaIndex = 0;

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
            _text.canvasRenderer.SetAlpha(_visibleAlfaIndex);
            _text.text = damage.ToString();
            _text.CrossFadeAlpha(_invisibleAlfaIndex, _textFadeDuration, false);
        }
    }
}
