using Agava.YandexGames;
using Scripts.Architecture.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Buttons
{
    public class RewardAddButton : MonoBehaviour
    {
        private readonly bool _isGameStopped = false;

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

        private void OnRewardButtonClick()
        {
            VideoAd.Show(OnOpenCallBack, OnRewardCallBack, OnCloseCallBack);
        }

        private void OnOpenCallBack()
        {
            _focusService.IsGameStopped = true;

            _focusService.PauseGame(_isGameStopped);
            _audioService.MuteAudio(_isGameStopped);
        }

        private void OnCloseCallBack()
        {
            _focusService.IsGameStopped = false;

            _focusService.PauseGame(!_isGameStopped);
            _audioService.MuteAudio(!_isGameStopped);
        }

        private void OnRewardCallBack()
        {
            _playerMoneyService.AddMoney(Constants.AddMoneyReward);
        }
    }
}
