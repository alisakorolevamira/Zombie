using Architecture.Factory;
using Architecture.Services;
using Architecture.Services.Data;
using Architecture.Services.GameLevel;
using Architecture.Services.Player;
using Architecture.Services.TimeScaleAndAudio;
using Architecture.Services.UI;
using Architecture.Services.Zombie;
using Architecture.ServicesInterfaces;
using Architecture.ServicesInterfaces.Data;
using Architecture.ServicesInterfaces.GameLevel;
using Architecture.ServicesInterfaces.Player;
using Architecture.ServicesInterfaces.TimeScaleAndAudio;
using Architecture.ServicesInterfaces.UI;
using Architecture.ServicesInterfaces.Zombie;
using Constants;
using UnityEngine;

namespace Architecture.States
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;
        private readonly AudioSource _audioSource;

        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, AllServices services, AudioSource audioSource)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            _audioSource = audioSource;

            RegisterService();
        }

        public async void Enter()
        {
            ISDKInitializeService sdkInitializeService = _services.Single<ISDKInitializeService>();
            IUIPanelService panelService = _services.Single<IUIPanelService>();
            ISpawnerService spawnerService = _services.Single<ISpawnerService>();
            ILocalizationService localizationService = _services.Single<ILocalizationService>();
            IFocusService focusService = _services.Single<IFocusService>();
            ILevelService levelService = _services.Single<ILevelService>();
            ICombatService combatService = _services.Single<ICombatService>();

            focusService.Initialize();
            spawnerService.Initialize();
            panelService.Initialize();
            levelService.Initialize();
            combatService.Initialize();

            await sdkInitializeService.StartCoroutineAsUniTask();
            
            localizationService.Initialize();

            _sceneLoader.Load(LevelConstants.Initial, EnterLoadLevel);
        }


        public void Exit() { }

        private void RegisterService()
        {
            _services.RegisterSingle<IAudioService>(new AudioService(_audioSource));
            _services.RegisterSingle<ITimeScaleService>(new TimeScaleService());
            
            _services.RegisterSingle<IFocusService>(new FocusService(
                _services.Single<IAudioService>(),
                _services.Single<ITimeScaleService>()));
            
            _services.RegisterSingle<ISDKInitializeService>(new SDKInitializeService());
            _services.RegisterSingle<IGameFactory>(new GameFactory());
            _services.RegisterSingle<ILevelService>(new LevelService());
            _services.RegisterSingle<IPlayerMoneyService>(new PlayerMoneyService());
            _services.RegisterSingle<IPlayerScoreService>(new PlayerScoreService());
            _services.RegisterSingle<ILevelDataService>(new LevelDataService(_services.Single<ILevelService>()));
            
            _services.RegisterSingle<IUIPanelService>(new UIPanelService(
                _gameStateMachine,
                _services.Single<IGameFactory>()));
            
            _services.RegisterSingle<IPlayerDataService>(new PlayerDataService(
                _services.Single<IPlayerMoneyService>(),
                _services.Single<IPlayerScoreService>()));
            
            _services.RegisterSingle<IZombieRewardService>(new ZombieRewardService(
                _services.Single<IPlayerMoneyService>(),
                _services.Single<IPlayerScoreService>()));
            
            _services.RegisterSingle<IZombieHealthService>(new ZombieHealthService(_services.Single<IZombieRewardService>()));
            
            _services.RegisterSingle<IZombieDataService>(new ZombieDataService(
                _services.Single<IZombieHealthService>(),
                _services.Single<IZombieRewardService>()));
            
            _services.RegisterSingle<ICardsPricesDataService>(new CardsPricesDataService(_services.Single<IUIPanelService>()));
            
            _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(
                _services.Single<IPlayerDataService>(),
                _services.Single<IZombieDataService>(),
                _services.Single<ILevelDataService>(),
                _services.Single<ICardsPricesDataService>()));
            
            _services.RegisterSingle<ILocalizationService>(new LocalizationService(_services.Single<IGameFactory>()));
            _services.RegisterSingle<ISpawnerService>(new SpawnerService(_services.Single<IGameFactory>()));
            _services.RegisterSingle<IStarCountService>(new StarCountService(_services.Single<IPlayerScoreService>()));
            
            _services.RegisterSingle<ICombatService>(new CombatService(
                _services.Single<ISpawnerService>(),
                _services.Single<IZombieHealthService>()));
        }

        private void EnterLoadLevel()
        {
            _gameStateMachine.Enter<LoadProgressState, string>(LevelConstants.Menu);
        }
    }
}