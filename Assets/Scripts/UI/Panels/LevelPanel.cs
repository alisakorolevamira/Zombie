using Scripts.Architecture.Services;
using Scripts.Architecture.States;
using Scripts.Audio;
using Scripts.UI.Cards;
using UnityEngine;

namespace Scripts.UI.Panels
{
    public class LevelPanel : Panel
    {
        [SerializeField] private Card[] _cards;
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

            foreach (var card in _cards)
                card.Open();
        }

        public override void Close()
        {
            base.Close();

            _backgroundAudio.StopAudio();

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