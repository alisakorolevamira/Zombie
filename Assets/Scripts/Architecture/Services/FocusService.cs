using Agava.WebUtility;
using UnityEngine;

namespace Scripts.Architecture.Services
{
    public class FocusService : IFocusService
    {
        private readonly IAudioService _audioService;

        public FocusService(IAudioService audioService)
        {
            _audioService = audioService;
            IsGameStopped = false;

            Application.focusChanged += OnInBackgroundChangeApp;
            WebApplication.InBackgroundChangeEvent += OnInBackgroundChangeWeb;
        }

        public bool IsGameStopped { get; set; }

        public void Dispose()
        {
            Application.focusChanged -= OnInBackgroundChangeApp;
            WebApplication.InBackgroundChangeEvent -= OnInBackgroundChangeWeb;
        }

        public void PauseGame(bool value)
        {
            if (value && !IsGameStopped)
                Time.timeScale = Constants.StartGameIndex;

            else
                Time.timeScale = Constants.StopGameIndex;
        }

        private void OnInBackgroundChangeApp(bool inApp)
        {
            _audioService.ChangeVolume(inApp);
            PauseGame(inApp);
        }

        private void OnInBackgroundChangeWeb(bool isBackground)
        {
            _audioService.ChangeVolume(!isBackground);
            PauseGame(!isBackground);
        }
    }
}