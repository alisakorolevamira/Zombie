using System;

namespace Scripts.Architecture.Services
{
    public interface IAudioService : IService
    {
        event Action VolumeChanged;

        bool IsMuted { get; }

        void ChangeVolume(bool value);
        void MuteAudio(bool value);
    }
}