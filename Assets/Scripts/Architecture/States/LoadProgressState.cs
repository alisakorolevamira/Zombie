using Scripts.Architecture.Services;

namespace Scripts.Architecture.States
{
    public class LoadProgressState : IPayLoadedState<string>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IPlayerProgressService _playerProgressService;
        private readonly IZombieProgressService _zombieProgressService;

        public LoadProgressState(GameStateMachine gameStateMachine, IPlayerProgressService playerProgressService, IZombieProgressService zombieProgressService)
        {
            _gameStateMachine = gameStateMachine;
            _playerProgressService = playerProgressService;
            _zombieProgressService = zombieProgressService;
        }

        public void Enter(string sceneName)
        {
            LoadProgress();
            _gameStateMachine.Enter<LoadLevelState, string>(sceneName);
        }

        public void Exit() { }

        private void LoadProgress()
        {
            _playerProgressService.LoadProgress();
            _zombieProgressService.LoadProgress();
        }
    }
}