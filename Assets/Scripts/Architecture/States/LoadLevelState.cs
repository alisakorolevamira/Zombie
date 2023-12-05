using Scripts.Spawner;
using Scripts.UI;
using System;
using UnityEngine;

namespace Scripts.Architecture.States
{
    public class LoadLevelState : IPayLoadedState<string>
    {
        private const string Menu = "Menu";
        private const string Initial = "Initial";
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly GameObject _spawner;
        private Action _sceneLoaded;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, GameObject spawner)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _spawner = spawner;
        }

        public void Enter(string sceneName)
        {
            LoadCanvas(sceneName);

            _sceneLoaded += OnLoaded;

            _sceneLoader.Load(sceneName, _sceneLoaded);
        }

        public void Exit()
        {
            _spawner.GetComponentInChildren<LoadingPanel>().Close();
        }

        private void OnLoaded()
        {
            _gameStateMachine.Enter<GameLoopState>();

            _sceneLoaded -= OnLoaded;
        }

        private void SpawnersOnLoaded()
        {
            _spawner.GetComponentInChildren<SitizenSpawner>().AddComponentsOnLevel();
            _spawner.GetComponent<ZombieSpawner>().CreateZombie();

            _sceneLoaded -= SpawnersOnLoaded;
        }

        private void LoadCanvas(string sceneName)
        {
            _spawner.GetComponentInChildren<PanelSpawner>().DisableAllPanels();

            LoadingPanel loadingPanel = _spawner.GetComponentInChildren<LoadingPanel>();
            loadingPanel.Open();

            _spawner.GetComponent<PanelSpawner>().CreateCanvas(sceneName);

            if (sceneName != Menu && sceneName != Initial)
            {
                _sceneLoaded += SpawnersOnLoaded;
            }
        }
    }
}