using Scripts.Architecture.Factory;

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
            Localization = _gameFactory.SpawnObject(Constants.LocalizationPath).GetComponent<Localization>();
            Localization.ChangeLanguage();
        }
    }
}