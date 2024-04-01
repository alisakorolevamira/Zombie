using Scripts.Architecture.Services;
using Scripts.Constants;
using System;

namespace Scripts.Architecture.States
{
    public class LoadLevelState : IPayLoadedState<string>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IUIPanelService _panelService;
        private readonly ISpawnerService _spawnerService;

        private string _sceneName;

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
            _panelService.LoadingPanel.Open();

            if (sceneName != LevelConstants.Menu && sceneName != LevelConstants.Initial)
                _sceneLoaded += SpawnersOnLoaded;

            _sceneLoaded += OnLoaded;

            _sceneName = sceneName;

            _sceneLoader.Load(sceneName, _sceneLoaded);
        }

        public void Exit() => _panelService.LoadingPanel.Close();

        private void OnLoaded()
        {
            _panelService.CreateCanvas(_sceneName);

            _gameStateMachine.Enter<GameLoopState>();

            _sceneLoaded -= OnLoaded;
        }

        private void SpawnersOnLoaded()
        {
            _spawnerService.CitizenSpawner.AddComponentsOnLevel();
            _spawnerService.ZombieSpawner.CreateZombie(_sceneName);

            _sceneLoaded -= SpawnersOnLoaded;
        }
    }
}