using Scripts.Architecture.Services;

namespace Scripts.Architecture.States
{
    public class LoadProgressState : IPayLoadedState<int>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IPlayerProgressService _playerProgressService;
        private readonly IZombieProgressService _zombieProgressService;
        private readonly ICardsPricesProgressService _cardsPricesProgressService;

        public LoadProgressState(GameStateMachine gameStateMachine, IPlayerProgressService playerProgressService,
            IZombieProgressService zombieProgressService, ICardsPricesProgressService cardsPricesProgressService)
        {
            _gameStateMachine = gameStateMachine;
            _playerProgressService = playerProgressService;
            _zombieProgressService = zombieProgressService;
            _cardsPricesProgressService = cardsPricesProgressService;
        }

        public void Enter(int sceneIndex)
        {
            LoadProgress();
            _gameStateMachine.Enter<LoadLevelState, int>(sceneIndex);
        }

        public void Exit() { }

        private void LoadProgress()
        {
            _playerProgressService.LoadProgress();
            _zombieProgressService.LoadProgress();
            _cardsPricesProgressService.LoadProgress();
        }
    }
}