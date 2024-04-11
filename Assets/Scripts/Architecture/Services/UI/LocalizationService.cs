using Architecture.Factory;
using Architecture.ServicesInterfaces.UI;
using Constants.UI;
using UI;

namespace Architecture.Services.UI
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