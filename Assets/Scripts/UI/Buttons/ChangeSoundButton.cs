using Scripts.Architecture.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Buttons
{
    [RequireComponent(typeof(Button))]

    public class ChangeSoundButton : MonoBehaviour
    {
        private IAudioService _audioService;

        [SerializeField] private Button _button;
        [SerializeField] private Image _image;
        [SerializeField] private Sprite _soundOnSprite;
        [SerializeField] private Sprite _soundOffSprite;

        private void OnEnable()
        {
            _button.onClick.AddListener(ChangeVolume);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(ChangeVolume);
            _audioService.VolumeChanged -= OnVolumeChanged;
        }

        private void Start()
        {
            _audioService = AllServices.Container.Single<IAudioService>();

            _audioService.VolumeChanged += OnVolumeChanged;
        }

        private void ChangeVolume()
        {
            if (_audioService.IsMuted)
                _audioService.ChangeVolume(true);

            else
                _audioService.ChangeVolume(false);
        }

        private void OnVolumeChanged()
        {
            if (_audioService.IsMuted)
                _image.sprite = _soundOffSprite;

            else
                _image.sprite = _soundOnSprite;
        }
    }
}
