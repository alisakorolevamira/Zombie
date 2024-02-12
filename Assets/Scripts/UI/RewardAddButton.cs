using Agava.YandexGames;
using Scripts.Architecture.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class RewardAddButton : MonoBehaviour
    {
        [SerializeField] private Button _rewardButton;

        private IPlayerMoneyService _playerMoneyService;

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
        }

        private void OnRewardButtonClick()
        {
            VideoAd.Show(OnOpenCallBack, OnRewardCallBack, OnCloseCallBack);
        }

        private void OnOpenCallBack()
        {
            Time.timeScale = 0;
            AudioListener.volume = 0f;
        }

        private void OnCloseCallBack()
        {
            Time.timeScale = 1;
            AudioListener.volume = 1f;
        }

        private void OnRewardCallBack()
        {
            _playerMoneyService.AddMoney(Constants.AddMoneyReward);
        }
    }
}
