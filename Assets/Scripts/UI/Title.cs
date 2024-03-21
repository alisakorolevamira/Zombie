using UnityEngine;
using UnityEngine.UI;
using Scripts.Architecture.Services;

namespace Scripts.UI
{

    public class Title : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Sprite _english;
        [SerializeField] private Sprite _russian;
        [SerializeField] private Sprite _turkish;

        private ILocalizationService _localizationService;

        private void OnEnable()
        {
            _localizationService = AllServices.Container.Single<ILocalizationService>();

            _localizationService.Localization.LanguageChanged += OnLanguageChanged;
        }

        private void OnDisable()
        {
            _localizationService.Localization.LanguageChanged -= OnLanguageChanged;
        }

        private void Start()
        {
            OnLanguageChanged();
        }

        private void OnLanguageChanged()
        {
            _image.sprite = _localizationService.Localization.CurrentLanguage switch
            {
                Constants.EnglishCode => _english,
                Constants.RussianCode => _russian,
                Constants.TurkishCode => _turkish,
                _ => _english,
            };
        }
    }
}
