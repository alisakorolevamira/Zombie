using Architecture.ServicesInterfaces.Data;

namespace Architecture.States
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

        public void Enter(string sceneName)
        {
            _saveLoadService.LoadProgress();
            _gameStateMachine.Enter<LoadLevelState, string>(sceneName);
        }

        public void Exit() { }
    }
}