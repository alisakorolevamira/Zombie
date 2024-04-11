using Architecture.Services;
using Architecture.ServicesInterfaces.UI;
using Constants.UI;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Title : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Sprite _english;
        [SerializeField] private Sprite _russian;
        [SerializeField] private Sprite _turkish;

        private ILocalizationService _localizationService;

        private void OnDisable()
        {
            if (_localizationService != null)
                _localizationService.Localization.LanguageChanged -= OnLanguageChanged;
        }

        private void Start()
        {
            _localizationService = AllServices.Container.Single<ILocalizationService>();

            _localizationService.Localization.LanguageChanged += OnLanguageChanged;
            OnLanguageChanged();
        }

        private void OnLanguageChanged()
        {
            _image.sprite = _localizationService.Localization.CurrentLanguage switch
            {
                LocalizationConstants.English => _english,
                LocalizationConstants.Russian => _russian,
                LocalizationConstants.Turkish => _turkish,
                _ => _english,
            };
        }
    }
}