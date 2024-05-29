using System.Threading;
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
        private CancellationTokenSource _cancellationTokenSource;

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

        private void OnButtonClick()
        {
            _rewardButton.interactable = false;

            VideoAd.Show(OnOpenCallBack, OnRewardCallBack, OnCloseCallBack);
        }

        private void OnRewardCallBack() => _playerMoneyService.AddMoney(AdConstants.AdMoneyReward);

        private void OnOpenCallBack()
        {
            _timeScaleService.Pause();
            _audioService.ChangeAudioByAd(true);
        }

        private void OnCloseCallBack()
        {
            _rewardButton.interactable = true;

            _timeScaleService.Continue();
            _audioService.ChangeAudioByAd(false);
        }
    }
}