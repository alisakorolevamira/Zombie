using Scripts.Architecture.Services;
using UnityEngine;

namespace Scripts.Audio
{
    public class BackgroundAudio : MonoBehaviour
    {
        [SerializeField] private AudioClip _audioClip;

        private IAudioService _audioService;

        private void Start()
        {
            _audioService = AllServices.Container.Single<IAudioService>();
        }

        public void PlayAudio()
        {
            _audioService.AudioSource.clip = _audioClip;
            _audioService.AudioSource.Play();
        }

        public void StopAudio() => _audioService?.AudioSource.Stop();
    }
}