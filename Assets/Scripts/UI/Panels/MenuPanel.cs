using Scripts.Architecture.Services;
using Scripts.Architecture.States;
using Scripts.Audio;
using System;
using UnityEngine;

namespace Scripts.UI.Panels
{
    public class MenuPanel : Panel
    {
        [SerializeField] private BackgroundAudio _backgroundAudio;

        private GameStateMachine _gameStateMachine;
        private IUIPanelService _panelService;

        public event Action Opened;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public override void Open()
        {
            base.Open();

            _panelService = AllServices.Container.Single<IUIPanelService>();

            _gameStateMachine = _panelService.StateMachine;
            _backgroundAudio.PlayAudio();
            Opened?.Invoke();
        }

        public override void Close()
        {
            base.Close();
            _backgroundAudio.StopAudio();
        }

        public void OpenLevel(string levelName) => _gameStateMachine.Enter<LoadProgressState, string>(levelName);
    }
}