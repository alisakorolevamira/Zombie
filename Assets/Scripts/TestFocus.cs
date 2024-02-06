using Agava.WebUtility;
using UnityEngine;

namespace Scripts
{
    public class TestFocus : MonoBehaviour
    {
        [SerializeField] private AudioSource[] _audioSourses;

        private void OnEnable()
        {
            Application.focusChanged += OnInBackgroundChangeApp;
            WebApplication.InBackgroundChangeEvent += OnInBackgroundChangeWeb;
        }

        private void OnDisable()
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
            foreach (var audioSourse in _audioSourses)
                audioSourse.volume = value ? 0 : 1;
        }

        private void PauseGame(bool value)
        {
            Time.timeScale = value ? 1 : 0;
        }
    }
}