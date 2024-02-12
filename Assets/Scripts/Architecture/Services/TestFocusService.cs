using Agava.WebUtility;
using UnityEngine;

namespace Scripts.Architecture.Services
{
    public class TestFocusService : ITestFocusService
    {
        public TestFocusService()
        {
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
            MuteAudio(!inApp);
            PauseGame(!inApp);
        }

        private void OnInBackgroundChangeWeb(bool isBackground)
        {
            MuteAudio(isBackground);
            PauseGame(isBackground);
        }

        private void MuteAudio(bool value)
        {
            AudioListener.volume = value ? 0 : 1;
        }

        private void PauseGame(bool value)
        {
            Time.timeScale = value ? 1 : 0;
        }
    }
}