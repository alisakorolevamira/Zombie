using Agava.YandexGames;
using Scripts.Architecture.Services;
using Scripts.Constants;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Buttons
{
    public class RewardAdButton : MonoBehaviour
    {
        [SerializeField] private Button _rewardButton;

        private IPlayerMoneyService _playerMoneyService;
        private IFocusService _focusService;
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
            _focusService = AllServices.Container.Single<IFocusService>();
            _audioService = AllServices.Container.Single<IAudioService>();
        }

        private void OnButtonClick() => VideoAd.Show(OnOpenCallBack, OnRewardCallBack, OnCloseCallBack);

        private void OnRewardCallBack() => _playerMoneyService.AddMoney(AdConstants.AdMoneyReward);

        private void OnOpenCallBack()
        {
            _focusService.IsGameStopped = true;
            _audioService.IsAdChangedAudio = true;

            _focusService.PauseGame(AdConstants.GameStopped);
            _audioService.MuteAudio(AdConstants.GameStopped);
        }

        private void OnCloseCallBack()
        {
            _focusService.IsGameStopped = false;
            _audioService.IsAdChangedAudio = false;

            _focusService.PauseGame(AdConstants.GameOn);
            _audioService.MuteAudio(AdConstants.GameOn);
        }
    }
}