using Architecture.ServicesInterfaces.TimeScaleAndAudio;
using Constants;
using UnityEngine;

namespace Architecture.Services.TimeScaleAndAudio
{
    public class TimeScaleService : ITimeScaleService
    {
        private bool _isGameStopped;

        public void Pause()
        {
            if (_isGameStopped)
                return;
            
            Time.timeScale = GameTimeScaleConstants.StopGameIndex;
            _isGameStopped = true;
        }

        public void Continue()
        {
            if (_isGameStopped == false)
                return;
            
            Time.timeScale = GameTimeScaleConstants.StartGameIndex;
            _isGameStopped = false;
        }
    }
}