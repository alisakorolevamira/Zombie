using Architecture.ServicesInterfaces.TimeScaleAndAudio;
using Constants;
using UnityEngine;

namespace Architecture.Services.TimeScaleAndAudio
{
    public class TimeScaleService : ITimeScaleService
    {
        public bool IsGameStopped { get; private set; }
        
        public void ChangeTimeScale(bool value)
        {
            if (value && !IsGameStopped)
                Time.timeScale = GameTimeScaleConstants.StartGameIndex;

            else
                Time.timeScale = GameTimeScaleConstants.StopGameIndex;
        }

        public void Pause()
        {
            if (IsGameStopped)
                return;
            
            Time.timeScale = GameTimeScaleConstants.StopGameIndex;
            IsGameStopped = true;
        }

        public void Continue()
        {
            if (IsGameStopped == false)
                return;
            
            Time.timeScale = GameTimeScaleConstants.StartGameIndex;
            IsGameStopped = false;
        }
    }
}