using Agava.YandexGames;
using Lean.Localization;
using UnityEngine;

namespace Scripts
{
    public class Localization : MonoBehaviour
    {
        private const string EnglishCode = "English";
        private const string RussianCode = "Russian";
        private const string TurkishCode = "Turkish";
        private const string English = "en";
        private const string Russian = "ru";
        private const string Turkish = "tr";

        private LeanLocalization _leanLanguage;

        private void Awake()
        {
            _leanLanguage = GetComponent<LeanLocalization>();
            //ChangeLanguage();
            DontDestroyOnLoad(gameObject);
        }

        private void ChangeLanguage()
        {
            string languageCode = YandexGamesSdk.Environment.i18n.lang;

            switch (languageCode)
            {
                case English:
                    _leanLanguage.SetCurrentLanguage(EnglishCode);
                    break;

                case Russian:
                    _leanLanguage.SetCurrentLanguage(RussianCode);
                    break;

                case Turkish:
                    _leanLanguage.SetCurrentLanguage(TurkishCode);
                    break;
            }
        }
    }
}

