using System;
using UnityEngine;

namespace Architecture.ServicesInterfaces.TimeScaleAndAudio
{
    public interface IAudioService : IService
    {
        event Action<bool> VolumeChanged;
        
        bool IsPlayerMutedAudio { get; set; }
        bool IsAdMutedAudio { get; set; }
        bool IsMuted { get; }
        
        void ChangeVolume(bool value);
        void PlayBackgroundAudio(AudioClip audioClip);
        void StopBackgroundAudio();
        void PlayShortEffectAudio(AudioClip audioClip);
    }
}