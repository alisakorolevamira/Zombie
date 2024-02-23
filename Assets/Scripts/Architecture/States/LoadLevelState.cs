using Scripts.Architecture.Services;
using System;

namespace Scripts.Architecture.States
{
    public class LoadLevelState : IPayLoadedState<string>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IUIPanelService _panelService;
        private readonly ISpawnerService _spawnerService;
        private Action _sceneLoaded;
        

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, IUIPanelService panelService,
            ISpawnerService spawnerService)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _panelService = panelService;
            _spawnerService = spawnerService;
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
            _spawnerService.SitizenSpawner.AddComponentsOnLevel();
            _spawnerService.ZombieSpawner.CreateZombie();

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