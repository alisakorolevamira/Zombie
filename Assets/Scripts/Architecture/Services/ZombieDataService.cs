using Scripts.Constants;
using Scripts.Progress;
using UnityEngine;

namespace Scripts.Architecture.Services
{
    public class ZombieDataService : IZombieDataService
    {
        private readonly IZombieHealthService _zombieHealthService;
        private readonly IZombieRewardService _zombieRewardService;

        private string _file;
        private ZombieProgress _zombieProgress = new();

        public ZombieDataService(IZombieHealthService zombieHealthService, IZombieRewardService zombieRewardService)
        {
            _zombieHealthService = zombieHealthService;
            _zombieRewardService = zombieRewardService;
        }

        public void LoadData()
        {
            _file = PlayerPrefs.GetString(ZombieConstants.Key, string.Empty);

            if (_file == string.Empty)
                SetNewData();

            else
                _zombieProgress = JsonUtility.FromJson<ZombieProgress>(_file);

            _zombieHealthService.Initialize(_zombieProgress);
            _zombieRewardService.Initialize(_zombieProgress);
        }

        public void ResetData()
        {
            PlayerPrefs.DeleteKey(ZombieConstants.Key);
            SetNewData();
        }

        public void SaveData()
        {
            _file = JsonUtility.ToJson(_zombieProgress);
            PlayerPrefs.SetString(ZombieConstants.Key, _file);
        }

        public void UpdateData()
        {
            _zombieProgress.Health = _zombieHealthService.Health;
            _zombieProgress.ScoreReward = _zombieRewardService.ScoreReward;
            _zombieProgress.MoneyReward = _zombieRewardService.MoneyReward;
        }

        private void SetNewData()
        {
            _zombieProgress.Health = ZombieConstants.ZombieMaximumHealth;
            _zombieProgress.ScoreReward = ZombieConstants.ZombieDefaultScoreReward;
            _zombieProgress.MoneyReward = ZombieConstants.ZombieDefaultMoneyReward;

            SaveData();
        }
    }
}