using Scripts.Architecture.Services;
using Scripts.Constants;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Buttons
{
    [RequireComponent(typeof(Button))]

    public class ChangeSoundButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;
        [SerializeField] private Sprite _soundOnSprite;
        [SerializeField] private Sprite _soundOffSprite;

        private IAudioService _audioService;

        private void OnEnable()
        {
            _button.onClick.AddListener(ChangeVolume);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(ChangeVolume);

            if (_audioService != null)
                _audioService.VolumeChanged -= OnVolumeChanged;
        }

        private void Start()
        {
            _audioService = AllServices.Container.Single<IAudioService>();

            _audioService.VolumeChanged += OnVolumeChanged;
        }

        private void ChangeVolume()
        {
            if (_audioService.AudioSource.mute)
                _audioService.ChangeVolume(GameFocusAndAudioConstants.VolumeOn);

            else
                _audioService.ChangeVolume(GameFocusAndAudioConstants.VolumeOff);
        }

        private void OnVolumeChanged()
        {
            if (_audioService.AudioSource.mute)
                _image.sprite = _soundOffSprite;

            else
                _image.sprite = _soundOnSprite;
        }
    }
}