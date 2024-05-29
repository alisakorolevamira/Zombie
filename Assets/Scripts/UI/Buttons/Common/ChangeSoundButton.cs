using Architecture.Services;
using Architecture.ServicesInterfaces.TimeScaleAndAudio;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Buttons.Common
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
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClick);

            if (_audioService != null)
                _audioService.VolumeChanged -= OnVolumeChanged;
        }

        private void Start()
        {
            _audioService = AllServices.Container.Single<IAudioService>();

            _audioService.VolumeChanged += OnVolumeChanged;
        }

        private void OnButtonClick() => _audioService.ChangeAudioByPlayer();

        private void OnVolumeChanged(bool value)
        {
            _image.sprite = _audioService.IsPaused ? _soundOffSprite : _soundOnSprite;
        }
    }
}