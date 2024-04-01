using Scripts.Architecture.Factory;
using Scripts.Constants;
using Scripts.UI;

namespace Scripts.Architecture.Services
{
    public class LocalizationService : ILocalizationService
    {
        private readonly IGameFactory _gameFactory;

        public LocalizationService(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        public Localization Localization { get; private set; }

        public void Initialize()
        {
            Localization = _gameFactory.SpawnObject(LocalizationConstants.LocalizationPath).GetComponent<Localization>();
            Localization.ChangeLanguage();
        }
    }
}