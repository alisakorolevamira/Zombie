using Architecture.Services;
using Architecture.ServicesInterfaces.TimeScaleAndAudio;
using UnityEngine;

namespace Audio
{
    public class BackgroundAudio : MonoBehaviour
    {
        [SerializeField] private AudioClip _audioClip;

        private IAudioService _audioService;

        private void Start()
        {
            _audioService = AllServices.Container.Single<IAudioService>();
        }

        public void PlayAudio() => _audioService?.PlayBackgroundAudio(_audioClip);

        public void StopAudio() => _audioService?.StopBackgroundAudio();
    }
}