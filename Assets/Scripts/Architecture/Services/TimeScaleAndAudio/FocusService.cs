using Agava.WebUtility;
using Architecture.ServicesInterfaces.TimeScaleAndAudio;
using UnityEngine;

namespace Architecture.Services.TimeScaleAndAudio
{
    public class FocusService : IFocusService
    {
        private readonly IAudioService _audioService;
        private readonly ITimeScaleService _timeScaleService;

        public FocusService(IAudioService audioService, ITimeScaleService timeScaleService)
        {
            _audioService = audioService;
            _timeScaleService = timeScaleService;
        }

        public void Initialize()
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
            _timeScaleService.ChangeTimeScale(inApp);
            
            if (_audioService.IsAdMutedAudio || _audioService.IsPlayerMutedAudio)
                return;

            _audioService.ChangeVolume(!inApp);
        }

        private void OnInBackgroundChangeWeb(bool isBackground)
        {
            _timeScaleService.ChangeTimeScale(!isBackground);
            
            if (_audioService.IsAdMutedAudio || _audioService.IsPlayerMutedAudio)
                return;

            _audioService.ChangeVolume(isBackground);
        }
    }
}