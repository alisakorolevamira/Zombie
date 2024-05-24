using Agava.YandexGames;
using Architecture.Services;
using Architecture.ServicesInterfaces.TimeScaleAndAudio;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Buttons.GameLevel
{
    public class InterstitialAdButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private ITimeScaleService _timeScaleService;
        private IAudioService _audioService;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }

        private void Start()
        {
            _timeScaleService = AllServices.Container.Single<ITimeScaleService>();
            _audioService = AllServices.Container.Single<IAudioService>();
        }

        private void OnButtonClick() => InterstitialAd.Show(OnOpenCallBack, OnCloseCallBack);

        private void OnOpenCallBack()
        {
            _timeScaleService.Pause();
            _audioService.ChangeAudioByAd(true);
        }

        private void OnCloseCallBack(bool closed)
        {
            _timeScaleService.Continue();
            _audioService.ChangeAudioByAd(false);
        }
    }
}