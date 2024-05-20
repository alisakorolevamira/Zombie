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
        
        public bool IsPaused { get; private set; }

        public void ChangeVolume(bool value)
        {
            _audioSource.mute = value;

            //VolumeChanged?.Invoke(value);
        }

        public void PlayBackgroundAudio(AudioClip audioClip)
        {
            _audioSource.clip = audioClip;
            _audioSource.Play();
        }

        public void StopBackgroundAudio() => _audioSource.Stop();

        public void PlayShortEffectAudio(AudioClip audioClip) => _audioSource.PlayOneShot(audioClip);

        public void Pause()
        {
            _audioSource.Pause();
            IsPaused = true;
            
            VolumeChanged?.Invoke(IsPaused);
        }

        public void Continue()
        {
            _audioSource.UnPause();
            IsPaused = false;
            
            VolumeChanged?.Invoke(IsPaused);
        }
    }
}