using Scripts.Architecture.Services;
using UnityEngine;

namespace Scripts.Audio
{
    public class ShortEffectAudio : MonoBehaviour
    {
        [SerializeField] private AudioClip _audioClip;

        private IAudioService _audioService;

        private void Start()
        {
            _audioService = AllServices.Container.Single<IAudioService>();
        }

        public void PlayOneShot() => _audioService?.AudioSource.PlayOneShot(_audioClip);
    }
}