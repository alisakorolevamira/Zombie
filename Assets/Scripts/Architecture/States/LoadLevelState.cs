using Scripts.Architecture.Services;
using Scripts.Spawner;
using System;

namespace Scripts.Architecture.States
{
    public class LoadLevelState : IPayLoadedState<string>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IUIPanelService _panelService;
        private readonly SitizenSpawner _sitizenSpawner;
        private readonly ZombieSpawner _zombieSpawner;
        private Action _sceneLoaded;
        

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, IUIPanelService panelService,
            SitizenSpawner sitizenSpawner, ZombieSpawner zombieSpawner)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _panelService = panelService;
            _sitizenSpawner = sitizenSpawner;
            _zombieSpawner = zombieSpawner;
        }

        public void Enter(string sceneName)
        {
            LoadCanvas(sceneName);

            _sceneLoaded += OnLoaded;

            _sceneLoader.Load(sceneName, _sceneLoaded);
        }

        public void Exit()
        {
            _panelService.LoadingPanel.Close();
        }

        private void OnLoaded()
        {
            _gameStateMachine.Enter<GameLoopState>();

            _sceneLoaded -= OnLoaded;
        }

        private void SpawnersOnLoaded()
        {
            _sitizenSpawner.AddComponentsOnLevel();
            _zombieSpawner.GetComponent<ZombieSpawner>().CreateZombie();

            _sceneLoaded -= SpawnersOnLoaded;
        }

        private void LoadCanvas(string sceneName)
        {
            _panelService.LoadingPanel.Open();
            _panelService.CreateCanvas(sceneName);


            if (sceneName != Constants.Menu && sceneName != Constants.Initial)
                _sceneLoaded += SpawnersOnLoaded;
        }
    }
}