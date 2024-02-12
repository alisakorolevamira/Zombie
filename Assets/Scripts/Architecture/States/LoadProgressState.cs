using Scripts.Architecture.Services;

namespace Scripts.Architecture.States
{
    public class LoadProgressState : IPayLoadedState<string>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly ISaveLoadService _saveLoadService;

        public LoadProgressState(GameStateMachine gameStateMachine, ISaveLoadService saveLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _saveLoadService = saveLoadService;
        }

        public async void Enter(string sceneName)
        {
            await _saveLoadService.LoadProgress();

            _gameStateMachine.Enter<LoadLevelState, string>(sceneName);
        }

        public void Exit() { }
    }
}