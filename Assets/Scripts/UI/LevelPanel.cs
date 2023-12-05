using Scripts.Architecture.Services;
using Scripts.Architecture.States;
using Scripts.Spawner;

namespace Scripts.UI
{
    public class LevelPanel : Panel
    {
        private GameStateMachine _gameStateMachine;
        private IPlayerProgressService _playerProgressService;
        private IZombieProgressService _zombieProgressService;
        private ISpawnerService _spawnerService;

        private void Start()
        {
            _playerProgressService = AllServices.Container.Single<IPlayerProgressService>();
            _zombieProgressService = AllServices.Container.Single<IZombieProgressService>();
            _spawnerService = AllServices.Container.Single<ISpawnerService>();
        }

        public void OpenNextScene(string sceneName)
        {
            _spawnerService.CurrentSitizenSpawner.ClearSubscriptions();
            _gameStateMachine = GetComponentInParent<PanelSpawner>().StateMachine;
            _gameStateMachine.Enter<LoadProgressState, string>(sceneName);
        }

        public void OpenSceneWithResetingProgress(string sceneName)
        {
            _playerProgressService.ResetProgress();
            _zombieProgressService.ResetProgress();

            OpenNextScene(sceneName);
        }
    }
}
