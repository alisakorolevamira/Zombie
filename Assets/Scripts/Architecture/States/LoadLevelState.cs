using Scripts.Spawner;
using Scripts.UI;
using System;
using UnityEngine;

namespace Scripts.Architecture.States
{
    public class LoadLevelState : IPayLoadedState<int>
    {
        private readonly int _menuIndex = 1;
        private readonly int _initialIndex = 0;

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

        public void Enter(int sceneIndex)
        {
            LoadCanvas(sceneIndex);

            _sceneLoaded += OnLoaded;

            _sceneLoader.Load(sceneIndex, _sceneLoaded);
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

        private void LoadCanvas(int sceneIndex)
        {
            _spawner.GetComponentInChildren<PanelSpawner>().DisableAllPanels();

            LoadingPanel loadingPanel = _spawner.GetComponentInChildren<LoadingPanel>();
            loadingPanel.Open();

            _spawner.GetComponent<PanelSpawner>().CreateCanvas(sceneIndex);

            if (sceneIndex != _menuIndex && sceneIndex != _initialIndex)
            {
                _sceneLoaded += SpawnersOnLoaded;
            }
        }
    }
}