using Scripts.Architecture.Services;
using Scripts.Architecture.States;
using Scripts.UI.Cards;
using UnityEngine;

namespace Scripts.UI.Panels
{
    public class LevelPanel : Panel
    {
        [SerializeField] private Card[] _cards;

        private GameStateMachine _gameStateMachine;
        private IPlayerDataService _playerDataService;
        private IZombieDataService _zombieDataService;
        private ICardsPricesDataService _cardsPricesDataService;
        private ILevelDataService _levelDataService;
        private IUIPanelService _panelService;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            _playerDataService = AllServices.Container.Single<IPlayerDataService>();
            _zombieDataService = AllServices.Container.Single<IZombieDataService>();
            _cardsPricesDataService = AllServices.Container.Single<ICardsPricesDataService>();
            _levelDataService = AllServices.Container.Single<ILevelDataService>();
            _panelService = AllServices.Container.Single<IUIPanelService>();
        }

        public override void Open()
        {
            base.Open();

            foreach (var card in _cards)
                card.Open();
        }

        public override void Close()
        {
            base.Close();

            foreach (var card in _cards)
                card.Close();
        }

        public void OpenNextScene(string sceneName)
        {
            Close();

            _gameStateMachine = _panelService.StateMachine;

            _gameStateMachine.Enter<LoadProgressState, string>(sceneName);
        }

        public void OpenNextSceneWithResetingProgress(string sceneName)
        {
            _playerDataService.ResetData(sceneName);
            _zombieDataService.ResetData();
            _cardsPricesDataService.ResetData();
            _levelDataService.UpdateData();

            OpenNextScene(sceneName);
        }
    }
}
