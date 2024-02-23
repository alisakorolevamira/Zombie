using Scripts.Architecture.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    [RequireComponent(typeof(Button))]

    public class ChangeSoundButton : MonoBehaviour
    {
        private IAudioService _audioService;

        [SerializeField] private Button _button;

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
            _audioService = AllServices.Container.Single<IAudioService>();
        }

        private void ChangeVolume()
        {
            _audioService.MuteAudio();
        }
    }
}
