using Agava.WebUtility;
using Scripts.Constants;
using UnityEngine;

namespace Scripts.Architecture.Services
{
    public class FocusService : IFocusService
    {
        private readonly IAudioService _audioService;

        public FocusService(IAudioService audioService)
        {
            _audioService = audioService;
        }

        public bool IsGameStopped { get; set; }

        public void Initialize()
        {
            IsGameStopped = false;

            Application.focusChanged += OnInBackgroundChangeApp;
            WebApplication.InBackgroundChangeEvent += OnInBackgroundChangeWeb;
        }

        public void Dispose()
        {
            Application.focusChanged -= OnInBackgroundChangeApp;
            WebApplication.InBackgroundChangeEvent -= OnInBackgroundChangeWeb;
        }

        public void PauseGame(bool value)
        {
            if (value && !IsGameStopped)
                Time.timeScale = GameFocusAndAudioConstants.StartGameIndex;

            else
                Time.timeScale = GameFocusAndAudioConstants.StopGameIndex;
        }

        private void OnInBackgroundChangeApp(bool inApp)
        {
            
            _audioService.MuteAudio(inApp);
            PauseGame(inApp);
        }

        private void OnInBackgroundChangeWeb(bool isBackground)
        {
            _audioService.MuteAudio(!isBackground);
            PauseGame(!isBackground);
        }
    }
}