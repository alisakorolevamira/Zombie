using System;
using Agava.YandexGames;
using Constants.UI;
using Lean.Localization;
using UnityEngine;

namespace UI
{
    public class Localization : MonoBehaviour
    {
        [SerializeField] private LeanLocalization _leanLanguage;

        public event Action LanguageChanged;
        public string CurrentLanguage => _leanLanguage.CurrentLanguage;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void ChangeLanguage(string languageCode = null)
        {
            if (languageCode == null)
                languageCode = YandexGamesSdk.Environment.i18n.lang;

            switch (languageCode)
            {
                case LocalizationConstants.EnglishCode:
                    _leanLanguage.SetCurrentLanguage(LocalizationConstants.English);
                    break;

                case LocalizationConstants.RussianCode:
                    _leanLanguage.SetCurrentLanguage(LocalizationConstants.Russian);
                    break;

                case LocalizationConstants.TurkishCode:
                    _leanLanguage.SetCurrentLanguage(LocalizationConstants.Turkish);
                    break;
            }

            LanguageChanged?.Invoke();
        }
    }
}