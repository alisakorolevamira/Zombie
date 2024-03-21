using System;
using UnityEngine;

namespace Scripts.Architecture.Services
{
    public class AudioService : IAudioService
    {
        public event Action VolumeChanged;
        public bool IsMuted { get; private set; }

        public AudioService()
        {
            IsMuted = false;
        }

        public void ChangeVolume(bool value)
        {
            AudioListener.volume = value ? Constants.MaximumVolumeValue : Constants.MinimumVolumeValue;
            IsMuted = !value;

            VolumeChanged?.Invoke();
        }

        public void MuteAudio(bool value)
        {
            if (IsMuted)
                return;

            else
                AudioListener.volume = value ? Constants.MaximumVolumeValue : Constants.MinimumVolumeValue;
        }
    }
}
