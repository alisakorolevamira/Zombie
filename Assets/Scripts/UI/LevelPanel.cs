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
        private ICardsPricesProgressService _cardsPricesProgressService;
        private ISpawnerService _spawnerService;

        private void Start()
        {
            _playerProgressService = AllServices.Container.Single<IPlayerProgressService>();
            _zombieProgressService = AllServices.Container.Single<IZombieProgressService>();
            _cardsPricesProgressService = AllServices.Container.Single<ICardsPricesProgressService>();
            _spawnerService = AllServices.Container.Single<ISpawnerService>();
        }

        public void OpenNextScene(int sceneIndex)
        {
            _spawnerService.CurrentSitizenSpawner.ClearSubscriptions();
            _gameStateMachine = GetComponentInParent<PanelSpawner>().StateMachine;
            _gameStateMachine.Enter<LoadProgressState, int>(sceneIndex);
        }

        public void OpenSceneWithResetingProgress(int sceneIndex)
        {
            _playerProgressService.ResetProgress();
            _zombieProgressService.ResetProgress();
            _cardsPricesProgressService.ResetProgress();

            OpenNextScene(sceneIndex);
        }
    }
}
