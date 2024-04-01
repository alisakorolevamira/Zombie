using Agava.YandexGames;
using Scripts.Architecture.Services;
using Scripts.Constants;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Buttons
{
    public class RewardAddButton : MonoBehaviour
    {
        [SerializeField] private Button _rewardButton;

        private IPlayerMoneyService _playerMoneyService;
        private IFocusService _focusService;
        private IAudioService _audioService;

        private void OnEnable()
        {
            _rewardButton.onClick.AddListener(OnRewardButtonClick);
        }

        private void OnDisable()
        {
            _rewardButton.onClick.RemoveListener(OnRewardButtonClick);
        }

        private void Start()
        {
            _playerMoneyService = AllServices.Container.Single<IPlayerMoneyService>();
            _focusService = AllServices.Container.Single<IFocusService>();
            _audioService = AllServices.Container.Single<IAudioService>();
        }

        private void OnRewardButtonClick() => VideoAd.Show(OnOpenCallBack, OnRewardCallBack, OnCloseCallBack);

        private void OnRewardCallBack() => _playerMoneyService.AddMoney(AddConstants.AddMoneyReward);

        private void OnOpenCallBack()
        {
            _focusService.IsGameStopped = true;

            _focusService.PauseGame(AddConstants.GameStopped);
            _audioService.MuteAudio(AddConstants.GameStopped);
        }

        private void OnCloseCallBack()
        {
            _focusService.IsGameStopped = false;

            _focusService.PauseGame(AddConstants.GameOn);
            _audioService.MuteAudio(AddConstants.GameOn);
        }
    }
}