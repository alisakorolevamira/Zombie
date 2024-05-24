using Architecture.Services;
using Architecture.ServicesInterfaces.Data;
using Architecture.ServicesInterfaces.UI;
using Architecture.States;
using Audio;
using UI.Buttons.GameLevel.Cards;
using UI.Panels.Common;
using UnityEngine;

namespace UI.Panels.GameLevel
{
    public class LevelPanel : Panel
    {
        [SerializeField] private Card[] _cards;
        [SerializeField] private RewardedAdButtonPanel _rewardAdButtonPanel;
        [SerializeField] private BackgroundAudio _backgroundAudio;

        private GameStateMachine _gameStateMachine;
        private IUIPanelService _panelService;
        private ISaveLoadService _saveLoadService;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
            _panelService = AllServices.Container.Single<IUIPanelService>();
            _gameStateMachine = _panelService.StateMachine;
        }

        public override void Open()
        {
            base.Open();

            _backgroundAudio.PlayAudio();
            _rewardAdButtonPanel.Open();

            foreach (var card in _cards)
                card.Open();
        }

        public override void Close()
        {
            base.Close();

            _backgroundAudio.StopAudio();
            _rewardAdButtonPanel.Close();

            foreach (var card in _cards)
                card.Close();
        }

        public void OpenNextScene(string sceneName)
        {
            Close();

            _gameStateMachine.Enter<LoadProgressState, string>(sceneName);
        }

        public void OpenNextSceneWithResetingProgress(string sceneName)
        {
            _saveLoadService.ResetProgressForNextLevel(sceneName);
            OpenNextScene(sceneName);
        }
    }
}