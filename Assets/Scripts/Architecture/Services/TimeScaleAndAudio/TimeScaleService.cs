using Architecture.ServicesInterfaces.TimeScaleAndAudio;
using Constants;
using UnityEngine;

namespace Architecture.Services.TimeScaleAndAudio
{
    public class TimeScaleService : ITimeScaleService
    {
        public bool IsGameStopped { get; set; } = false;
        
        public void ChangeTimeScale(bool value)
        {
            if (value && !IsGameStopped)
                Time.timeScale = GameTimeScaleConstants.StartGameIndex;

            else
                Time.timeScale = GameTimeScaleConstants.StopGameIndex;
        }
    }
}