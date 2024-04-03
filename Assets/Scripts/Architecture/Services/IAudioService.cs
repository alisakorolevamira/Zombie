using System;
using UnityEngine;

namespace Scripts.Architecture.Services
{
    public interface IAudioService : IService
    {
        bool IsAdChangedAudio { get; set; }
        event Action VolumeChanged;

        AudioSource AudioSource { get; }

        void ChangeVolume(bool value);
        void MuteAudio(bool value);
    }
}