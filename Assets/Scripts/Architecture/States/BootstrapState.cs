using Scripts.Architecture.Services;
using Scripts.Architecture.Factory;

namespace Scripts.Architecture.States
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _services = services;

            RegisterService();
        }

        public async void Enter()
        {
            var sdkInitializeService = _services.Single<ISDKInitializeService>();
            var panelService = _services.Single<IUIPanelService>();
            var spawnerService = _services.Single<ISpawnerService>();
            var localizationService = _services.Single<ILocalizationService>();
            var focusService = _services.Single<IFocusService>();
            var levelService = _services.Single<ILevelService>();
            var combatService = _services.Single<ICombatService>();

            spawnerService.Initialize();
            panelService.Initialize();
            focusService.Initialize();
            levelService.Initialize();
            combatService.Initialize();

            await sdkInitializeService.StartCoroutineAsUniTask();
            localizationService.Initialize();

            _sceneLoader.Load(Constants.Initial, EnterLoadLevel);
        }


        public void Exit() { }

        private void RegisterService()
        {
            _services.RegisterSingle<ISDKInitializeService>(new SDKInitializeService());
            _services.RegisterSingle<IGameFactory>(new GameFactory());
            _services.RegisterSingle<IAudioService>(new AudioService());
            _services.RegisterSingle<ILevelService>(new LevelService());
            _services.RegisterSingle<IPlayerMoneyService>(new PlayerMoneyService());
            _services.RegisterSingle<IPlayerScoreService>(new PlayerScoreService());
            _services.RegisterSingle<ILevelDataService>(new LevelDataService(_services.Single<ILevelService>()));
            _services.RegisterSingle<IUIPanelService>(new UIPanelService(_gameStateMachine, _services.Single<IGameFactory>()));
            _services.RegisterSingle<IPlayerDataService>(new PlayerDataService(_services.Single<IPlayerMoneyService>(),
                _services.Single<IPlayerScoreService>()));
            _services.RegisterSingle<IZombieRewardService>(new ZombieRewardService(_services.Single<IPlayerMoneyService>(),
                _services.Single<IPlayerScoreService>()));
            _services.RegisterSingle<IZombieHealthService>(new ZombieHealthService(_services.Single<IZombieRewardService>()));
            _services.RegisterSingle<IZombieDataService>(new ZombieDataService(_services.Single<IZombieHealthService>(),
                _services.Single<IZombieRewardService>()));
            _services.RegisterSingle<ICardsPricesDataService>(new CardsPricesDataService(_services.Single<IUIPanelService>()));
            _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(_services.Single<IPlayerDataService>(),
                _services.Single<IZombieDataService>(), _services.Single<ILevelDataService>(), _services.Single<ICardsPricesDataService>()));
            _services.RegisterSingle<ILocalizationService>(new LocalizationService(_services.Single<IGameFactory>()));
            _services.RegisterSingle<ISpawnerService>(new SpawnerService(_services.Single<IGameFactory>()));
            _services.RegisterSingle<IFocusService>(new FocusService(_services.Single<IAudioService>()));
            _services.RegisterSingle<IStarCountService>(new StarCountService(_services.Single<IPlayerScoreService>()));
            _services.RegisterSingle<ICombatService>(new CombatService(_services.Single<ISpawnerService>()));
        }

        private void EnterLoadLevel()
        {
            _gameStateMachine.Enter<LoadProgressState, string>(Constants.Menu);
        }
    }
}
