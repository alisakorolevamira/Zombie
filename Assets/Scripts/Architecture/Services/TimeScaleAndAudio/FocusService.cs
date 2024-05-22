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
            if (WebApplication.IsRunningOnWebGL == false)
                return;

            OnInBackgroundChangeWeb(WebApplication.InBackground);
            OnInBackgroundChangeApp(Application.isFocused);
            
            Application.focusChanged += OnInBackgroundChangeApp;
            WebApplication.InBackgroundChangeEvent += OnInBackgroundChangeWeb;
        }

        public void Dispose()
        {
            if (WebApplication.IsRunningOnWebGL == false)
                return;
            
            Application.focusChanged -= OnInBackgroundChangeApp;
            WebApplication.InBackgroundChangeEvent -= OnInBackgroundChangeWeb;
        }
        
        private void OnInBackgroundChangeApp(bool inApp)
        {
            if (inApp == false)
                _timeScaleService.Pause();

            else
                _timeScaleService.Continue();
            
            _audioService.ChangeAudioByFocus(inApp);
        }

        private void OnInBackgroundChangeWeb(bool isBackground)
        {
            if (isBackground)
                _timeScaleService.Pause();

            else
                _timeScaleService.Continue();
            
            _audioService.ChangeAudioByFocus(!isBackground);
        }
    }
}