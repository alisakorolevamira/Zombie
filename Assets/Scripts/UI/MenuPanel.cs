using Scripts.Architecture.Services;
using Scripts.Architecture.States;
using Scripts.Spawner;

namespace Scripts.UI
{
    public class MenuPanel : Panel
    {
        private GameStateMachine _gameStateMachine;
        private IPlayerProgressService _playerProgressService;
        private IZombieProgressService _zombieProgressService;

        private void Start()
        {
            _playerProgressService = AllServices.Container.Single<IPlayerProgressService>();
            _zombieProgressService = AllServices.Container.Single<IZombieProgressService>();
        }

        public void OpenAnyLevel(string sceneName)
        {
            _playerProgressService.ResetProgress();
            _zombieProgressService.ResetProgress();

            _gameStateMachine = GetComponentInParent<PanelSpawner>().StateMachine;
            _gameStateMachine.Enter<LoadProgressState, string>(sceneName);
        }

        public void OpenProgressLevel()
        {
            _gameStateMachine = GetComponentInParent<PanelSpawner>().StateMachine;
            _gameStateMachine.Enter<LoadProgressState, string>(_playerProgressService.Progress.Level);
        }
    }
}
