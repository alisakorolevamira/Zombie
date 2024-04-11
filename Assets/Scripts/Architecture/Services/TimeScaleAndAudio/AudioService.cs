using System;
using Architecture.ServicesInterfaces.TimeScaleAndAudio;
using UnityEngine;

namespace Architecture.Services.TimeScaleAndAudio
{
    public class AudioService : IAudioService
    {
        private readonly AudioSource _audioSource;
        
        public event Action<bool> VolumeChanged;

        public AudioService(AudioSource audioSource)
        {
            _audioSource = audioSource;
            _audioSource.mute = false;
        }

        public bool IsPlayerMutedAudio { get; set; } = false;
        public bool IsAdMutedAudio { get; set; } = false;
        public bool IsMuted => _audioSource.mute;

        public void ChangeVolume(bool value)
        {
            _audioSource.mute = value;

            VolumeChanged?.Invoke(value);
        }

        public void PlayBackgroundAudio(AudioClip audioClip)
        {
            _audioSource.clip = audioClip;
            _audioSource.Play();
        }

        public void StopBackgroundAudio() => _audioSource.Stop();

        public void PlayShortEffectAudio(AudioClip audioClip) => _audioSource.PlayOneShot(audioClip);
    }
}