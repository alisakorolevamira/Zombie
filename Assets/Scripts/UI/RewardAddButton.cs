using Agava.YandexGames;
using Scripts.Architecture.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class RewardAddButton : MonoBehaviour
    {
        private readonly bool _isSoundOn = true;

        [SerializeField] private Button _rewardButton;

        private IPlayerMoneyService _playerMoneyService;
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
            _audioService = AllServices.Container.Single<IAudioService>();
        }

        private void OnRewardButtonClick()
        {
            VideoAd.Show(OnOpenCallBack, OnRewardCallBack, OnCloseCallBack);
        }

        private void OnOpenCallBack()
        {
            Time.timeScale = Constants.StopGameIndex;
            _audioService.ChangeVolume(!_isSoundOn);
        }

        private void OnCloseCallBack()
        {
            Time.timeScale = Constants.StartGameIndex;
            _audioService.ChangeVolume(_isSoundOn);
        }

        private void OnRewardCallBack()
        {
            _playerMoneyService.AddMoney(Constants.AddMoneyReward);
        }
    }
}
