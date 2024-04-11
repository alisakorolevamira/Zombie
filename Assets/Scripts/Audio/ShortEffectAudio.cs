using Architecture.Services;
using Architecture.ServicesInterfaces.TimeScaleAndAudio;
using UnityEngine;

namespace Audio
{
    public class ShortEffectAudio : MonoBehaviour
    {
        [SerializeField] private AudioClip _audioClip;

        private IAudioService _audioService;

        private void Start()
        {
            _audioService = AllServices.Container.Single<IAudioService>();
        }

        public void PlayOneShot() => _audioService?.PlayShortEffectAudio(_audioClip);
    }
}