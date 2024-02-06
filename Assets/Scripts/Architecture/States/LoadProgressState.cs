using Scripts.Architecture.Services;

namespace Scripts.Architecture.States
{
    public class LoadProgressState : IPayLoadedState<int>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly ISaveLoadService _saveLoadService;

        public LoadProgressState(GameStateMachine gameStateMachine, ISaveLoadService saveLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _saveLoadService = saveLoadService;
        }

        public void Enter(int sceneIndex)
        {
            _saveLoadService.LoadProgress();
            _gameStateMachine.Enter<LoadLevelState, int>(sceneIndex);
        }

        public void Exit() { }
    }
}