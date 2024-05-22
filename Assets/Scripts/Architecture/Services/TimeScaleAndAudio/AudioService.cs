using System;
using Architecture.ServicesInterfaces.TimeScaleAndAudio;
using UnityEngine;

namespace Architecture.Services.TimeScaleAndAudio
{
    public class AudioService : IAudioService
    {
        private readonly AudioSource _audioSource;

        private bool _isAdChangedAudio;
        private bool _isPlayerChangedAudio;
        private bool _isFocusChangedAudio;
        
        public event Action<bool> VolumeChanged;

        public AudioService(AudioSource audioSource)
        {
            _audioSource = audioSource;
            IsPaused = false;
            _isAdChangedAudio = false;
            _isPlayerChangedAudio = false;
        }

        public bool IsPaused { get; private set; }

        public void PlayBackgroundAudio(AudioClip audioClip)
        {
            _audioSource.clip = audioClip;

            if (_isPlayerChangedAudio || _isAdChangedAudio)
                return;
            
            _audioSource.Play();

        }

        public void StopBackgroundAudio() => _audioSource.Stop();

        public void PlayShortEffectAudio(AudioClip audioClip)
        {
            if (_isPlayerChangedAudio || _isAdChangedAudio)
                return;
            
            _audioSource.PlayOneShot(audioClip);
        }

        public void ChangeAudioByAd(bool isAdOn)
        {
            if (_isPlayerChangedAudio)
                return;

            if (isAdOn)
            {
                Pause();
                _isAdChangedAudio = true;
            }

            else
            {
                Continue();
                _isAdChangedAudio = false;
            }
        }

        public void ChangeAudioByPlayer()
        {
            if (_isPlayerChangedAudio == false)
            {
                _isPlayerChangedAudio = true;
                Pause();
            }

            else
            {
                _isPlayerChangedAudio = false;
                Continue();
            }
        }

        public void ChangeAudioByFocus(bool inApp)
        {
            if (_isPlayerChangedAudio || _isAdChangedAudio)
                return;

            if (inApp)
                Continue();

            else
                Pause();
        }

        private void Pause()
        {
            _audioSource.Pause();
            IsPaused = true;
            VolumeChanged?.Invoke(IsPaused);
        }

        private void Continue()
        {
            _audioSource.Play();
            IsPaused = false;
            VolumeChanged?.Invoke(IsPaused);
        }
        
        
    }
}