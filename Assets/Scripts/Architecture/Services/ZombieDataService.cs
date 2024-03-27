using Scripts.Progress;
using UnityEngine;

namespace Scripts.Architecture.Services
{
    public class ZombieDataService : IZombieDataService
    {
        private readonly IZombieHealthService _zombieHealthService;
        private readonly IZombieRewardService _zombieRewardService;
        private readonly string _key = "ZombieProgress";

        private string _file;
        private ZombieProgress _zombieProgress = new();

        public ZombieDataService(IZombieHealthService zombieHealthService, IZombieRewardService zombieRewardService)
        {
            _zombieHealthService = zombieHealthService;
            _zombieRewardService = zombieRewardService;
        }

        public void LoadData()
        {
            _file = PlayerPrefs.GetString(_key, string.Empty);

            if (_file == string.Empty)
                SetNewData();

            else
                _zombieProgress = JsonUtility.FromJson<ZombieProgress>(_file);

            _zombieHealthService.Initialize(_zombieProgress);
            _zombieRewardService.Initialize(_zombieProgress);
        }

        public void ResetData()
        {
            PlayerPrefs.DeleteKey(_key);

            SetNewData();
        }

        public void SaveData()
        {
            _file = JsonUtility.ToJson(_zombieProgress);
            PlayerPrefs.SetString(_key, _file);
        }

        public void UpdateData()
        {
            _zombieProgress.Health = _zombieHealthService.Health;
            _zombieProgress.ScoreReward = _zombieRewardService.ScoreReward;
            _zombieProgress.MoneyReward = _zombieRewardService.MoneyReward;
        }

        private void SetNewData()
        {
            _zombieProgress.Health = Constants.ZombieMaximumHealth;
            _zombieProgress.ScoreReward = Constants.ZombieDefaultScoreReward;
            _zombieProgress.MoneyReward = Constants.ZombieDefaultMoneyReward;

            SaveData();
        }
    }
}
