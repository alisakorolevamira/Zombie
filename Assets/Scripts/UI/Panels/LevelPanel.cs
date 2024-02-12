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
        private ISaveLoadService _saveLoadService;
        private ISpawnerService _spawnerService;
        private IUIPanelService _panelService;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
            _spawnerService = AllServices.Container.Single<ISpawnerService>();
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
            _spawnerService.CurrentSitizenSpawner.ClearSubscriptions();
            _gameStateMachine = _panelService.StateMachine;
            _gameStateMachine.Enter<LoadProgressState, string>(sceneName);
        }

        public void OpenSceneWithResetingProgress(string sceneName)
        {
            _saveLoadService.ResetProgress();
            _saveLoadService.SaveProgress();
            OpenNextScene(sceneName);
        }
    }
}
