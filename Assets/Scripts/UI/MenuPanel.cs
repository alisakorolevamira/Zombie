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
        private ICardsPricesProgressService _cardsPricesProgressService;

        private void Start()
        {
            _playerProgressService = AllServices.Container.Single<IPlayerProgressService>();
            _zombieProgressService = AllServices.Container.Single<IZombieProgressService>();
            _cardsPricesProgressService = AllServices.Container.Single<ICardsPricesProgressService>();
        }

        public void OpenAnyLevel(int sceneIndex)
        {
            _playerProgressService.ResetProgress();
            _zombieProgressService.ResetProgress();
            _cardsPricesProgressService.ResetProgress();

            _gameStateMachine = GetComponentInParent<PanelSpawner>().StateMachine;
            _gameStateMachine.Enter<LoadProgressState, int>(sceneIndex);
        }

        public void OpenProgressLevel()
        {
            _gameStateMachine = GetComponentInParent<PanelSpawner>().StateMachine;
            _gameStateMachine.Enter<LoadProgressState, int>(_playerProgressService.Progress.Level);
        }
    }
}
