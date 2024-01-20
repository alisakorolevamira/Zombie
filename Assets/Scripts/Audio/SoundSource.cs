using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Audio
{
    [RequireComponent(typeof(AudioSource))]

    public class SoundSource : MonoBehaviour
    {
        [SerializeField] private Button _button;
        private readonly int _minimumVolume = 0;
        private readonly int _maximumVolume = 1;
        private AudioSource _audioSource;

        private void OnEnable()
        {
            _button.onClick.AddListener(ChangeVolume);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(ChangeVolume);
        }

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void ChangeVolume()
        {
            if (_audioSource != null)
            {
                if (_audioSource.volume == _maximumVolume)
                {
                    _audioSource.volume = _minimumVolume;
                }

                else
                {
                    _audioSource.volume = _maximumVolume;
                }
            }
        }
    }
}
