using System;
using System.Threading;
using Agava.YandexGames;
using Architecture.Services;
using Architecture.ServicesInterfaces.Player;
using Architecture.ServicesInterfaces.TimeScaleAndAudio;
using Constants.UI;
using Cysharp.Threading.Tasks;
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
            _cancellationTokenSource = new CancellationTokenSource();
            _rewardButton.onClick.AddListener(OnButtonClick);
            StartInitDelay();
        }

        private void OnDisable()
        {
            _cancellationTokenSource.Cancel();
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
            _timeScaleService.Pause();
            _audioService.Pause();
        }

        private void OnCloseCallBack()
        {
            _timeScaleService.Continue();
            _audioService.Continue();
        }

        private async void StartInitDelay()
        {
            try
            {
                _rewardButton.gameObject.SetActive(false);
                await UniTask.Delay(TimeSpan.FromSeconds(3), cancellationToken:_cancellationTokenSource.Token);
                _rewardButton.gameObject.SetActive(true);
            }
            catch (OperationCanceledException)
            {
                _rewardButton.gameObject.SetActive(true);
            }
        }
    }
}