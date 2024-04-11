using Agava.YandexGames;
using Architecture.Services;
using Architecture.ServicesInterfaces.TimeScaleAndAudio;
using Constants.UI;
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

        private void OnButtonClick()
        {
            InterstitialAd.Show(OnOpenCallBack, OnCloseCallBack);
        }

        private void OnOpenCallBack()
        {
            _timeScaleService.IsGameStopped = true;
            _timeScaleService.ChangeTimeScale(AdConstants.GameStopped);
            
            if(_audioService.IsPlayerMutedAudio)
                return;
            
            _audioService.IsAdMutedAudio = true;
            _audioService.ChangeVolume(!AdConstants.GameStopped);
        }

        private void OnCloseCallBack(bool closed)
        {
            _timeScaleService.IsGameStopped = false;
            _timeScaleService.ChangeTimeScale(AdConstants.GameOn);

            if (_audioService.IsPlayerMutedAudio)
                return;
            
            _audioService.IsAdMutedAudio = false;
            _audioService.ChangeVolume(!AdConstants.GameOn);
        }
    }
}