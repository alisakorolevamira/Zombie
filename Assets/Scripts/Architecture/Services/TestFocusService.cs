using Agava.WebUtility;
using UnityEngine;

namespace Scripts.Architecture.Services
{
    public class TestFocusService : ITestFocusService
    {
        private readonly IAudioService _audioService;

        public TestFocusService(IAudioService audioService)
        {
            _audioService = audioService;

            Application.focusChanged += OnInBackgroundChangeApp;
            WebApplication.InBackgroundChangeEvent += OnInBackgroundChangeWeb;
        }

        public void Dispose()
        {
            Application.focusChanged -= OnInBackgroundChangeApp;
            WebApplication.InBackgroundChangeEvent -= OnInBackgroundChangeWeb;
        }

        private void OnInBackgroundChangeApp(bool inApp)
        {
            _audioService.ChangeVolume(!inApp);
            PauseGame(!inApp);
        }

        private void OnInBackgroundChangeWeb(bool isBackground)
        {
            _audioService.ChangeVolume(isBackground);
            PauseGame(isBackground);
        }

        private void PauseGame(bool value)
        {
            Time.timeScale = value ? Constants.StartGameIndex : Constants.StopGameIndex;
        }
    }
}