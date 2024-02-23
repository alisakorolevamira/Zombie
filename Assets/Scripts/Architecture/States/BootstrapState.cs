using Scripts.Architecture.Services;
using Scripts.Architecture.Factory;
using Scripts.Spawner;
using Scripts.UI.Panels;

namespace Scripts.Architecture.States
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;
        private readonly SitizenSpawner _sitizenSpawner;
        private readonly ZombieSpawner _zombieSpawner;
        private readonly LevelPanel _levelPanel;
        private readonly LoadingPanel _loadingPanel;
        private readonly Localization _localization;

        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, AllServices services, 
            SitizenSpawner sitizenSpawner, ZombieSpawner zombieSpawner, LevelPanel levelPanel, LoadingPanel loadingPanel, Localization localization)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            _sitizenSpawner = sitizenSpawner;
            _zombieSpawner = zombieSpawner;
            _levelPanel = levelPanel;
            _loadingPanel = loadingPanel;
            _localization = localization;

            RegisterService();
        }

        public void Enter()
        {
            _sceneLoader.Load(Constants.Initial, EnterLoadLevel);
        }


        public void Exit() { }

        private void RegisterService()
        {
            //_services.RegisterSingle<ISDKInitializer>(new SDKInitializer());
            //var initializer = _services.Single<ISDKInitializer>();
            //await initializer.RunCoroutineAsTask();

            _services.RegisterSingle<IAudioService>(new AudioService());
            _services.RegisterSingle<ITestFocusService>(new TestFocusService(_services.Single<IAudioService>()));
            _services.RegisterSingle<IUIPanelService>(new UIPanelService(_gameStateMachine, _levelPanel, _loadingPanel));
            _services.RegisterSingle<IGameFactory>(new GameFactory());
            _services.RegisterSingle<ISaveLoadService>(new SaveLoadService());
            _services.RegisterSingle<IPlayerMoneyService>(new PlayerMoneyService(_services.Single<ISaveLoadService>()));
            _services.RegisterSingle<IPlayerScoreService>(new PlayerScoreService(_services.Single<ISaveLoadService>()));
            _services.RegisterSingle<IZombieRewardService>(new ZombieRewardService(_services.Single<IPlayerMoneyService>(),
                _services.Single<IPlayerScoreService>(), _services.Single<ISaveLoadService>()));
            _services.RegisterSingle<IZombieHealthService>(new ZombieHealthService(_services.Single<IZombieRewardService>(),
                _services.Single<ISaveLoadService>()));
            _services.RegisterSingle<ISpawnerService>(new SpawnerService(_sitizenSpawner, _zombieSpawner));

            _localization.ChangeLanguage();
        }

        private void EnterLoadLevel()
        {
            _gameStateMachine.Enter<LoadProgressState, string>(Constants.Menu);
        }
    }
}
