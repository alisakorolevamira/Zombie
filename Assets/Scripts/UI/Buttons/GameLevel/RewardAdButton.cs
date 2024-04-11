using Agava.YandexGames;
using Architecture.Services;
using Architecture.ServicesInterfaces.Player;
using Architecture.ServicesInterfaces.TimeScaleAndAudio;
using Constants.UI;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Buttons.GameLevel
{
    public class RewardAdButton : MonoBehaviour
    {
        [SerializeField] private Button _rewardButton;

        private IPlayerMoneyService _playerMoneyService;
        private ITimeScaleService _timeScaleService;
        private IAudioService _audioService;

        private void OnEnable()
        {
            _rewardButton.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            _rewardButton.onClick.RemoveListener(OnButtonClick);
        }

        private void Start()
        {
            _playerMoneyService = AllServices.Container.Single<IPlayerMoneyService>();
            _timeScaleService = AllServices.Container.Single<ITimeScaleService>();
            _audioService = AllServices.Container.Single<IAudioService>();
        }

        private void OnButtonClick() => VideoAd.Show(OnOpenCallBack, OnRewardCallBack, OnCloseCallBack);

        private void OnRewardCallBack() => _playerMoneyService.AddMoney(AdConstants.AdMoneyReward);

        private void OnOpenCallBack()
        {
            _timeScaleService.IsGameStopped = true;
            _timeScaleService.ChangeTimeScale(AdConstants.GameStopped);
            
            if(_audioService.IsPlayerMutedAudio)
                return;
            
            _audioService.IsAdMutedAudio = true;
            _audioService.ChangeVolume(!AdConstants.GameStopped);
        }

        private void OnCloseCallBack()
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