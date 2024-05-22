using System;
using UnityEngine;

namespace Architecture.ServicesInterfaces.TimeScaleAndAudio
{
    public interface IAudioService : IService
    {
        event Action<bool> VolumeChanged;
        
        bool IsPaused { get; }

        void ChangeAudioByPlayer();
        void ChangeAudioByAd(bool isAdOn);
        void ChangeAudioByFocus(bool inApp);
        void PlayBackgroundAudio(AudioClip audioClip);
        void PlayShortEffectAudio(AudioClip audioClip);
        void StopBackgroundAudio();
    }
}