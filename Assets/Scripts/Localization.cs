using Agava.YandexGames;
using Lean.Localization;
using System;
using UnityEngine;

namespace Scripts
{
    public class Localization : MonoBehaviour
    {
        private const string English = "en";
        private const string Russian = "ru";
        private const string Turkish = "tr";

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
                case English:
                    _leanLanguage.SetCurrentLanguage(Constants.EnglishCode);
                    break;

                case Russian:
                    _leanLanguage.SetCurrentLanguage(Constants.RussianCode);
                    break;

                case Turkish:
                    _leanLanguage.SetCurrentLanguage(Constants.TurkishCode);
                    break;
            }

            LanguageChanged?.Invoke();
        }
    }
}

