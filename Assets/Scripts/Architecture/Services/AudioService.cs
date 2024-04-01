using Scripts.Constants;
using System;
using UnityEngine;

namespace Scripts.Architecture.Services
{
    public class AudioService : IAudioService
    {
        public event Action VolumeChanged;

        public AudioService(AudioSource audioSource)
        {
            AudioSource = audioSource;
            AudioSource.mute = false;
        }

        public AudioSource AudioSource { get; private set; }

        public void ChangeVolume(bool value)
        {
            AudioSource.volume = value ? GameFocusAndAudioConstants.MaximumVolumeValue : GameFocusAndAudioConstants.MinimumVolumeValue;
            AudioSource.mute = !value;

            VolumeChanged?.Invoke();
        }

        public void MuteAudio(bool value)
        {
            if (AudioSource.mute)
                return;

            else
                AudioSource.volume = value ? GameFocusAndAudioConstants.MaximumVolumeValue : GameFocusAndAudioConstants.MinimumVolumeValue;
        }
    }
}