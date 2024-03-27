using Scripts.Progress;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Architecture.Services
{
    public class PlayerDataService : IPlayerDataService
    {
        private readonly IPlayerMoneyService _playerMoneyService;
        private readonly IPlayerScoreService _playerScoreService;
        private readonly string _key = "PlayerProgress";

        private string _file;

        public PlayerDataService(IPlayerMoneyService playerMoneyService, IPlayerScoreService playerScoreService)
        {
            _playerMoneyService = playerMoneyService;
            _playerScoreService = playerScoreService;
        }

        public PlayerProgress PlayerProgress { get; private set; } = new();

        public void LoadData()
        {
            _file = PlayerPrefs.GetString(_key, string.Empty);

            if (_file == string.Empty)
                SetNewData(Constants.Menu);

            else
                PlayerProgress = JsonUtility.FromJson<PlayerProgress>(_file);

            _playerMoneyService.Initialize(PlayerProgress);
            _playerScoreService.Initialize(PlayerProgress);
        }

        public void ResetData(string levelName)
        {
            PlayerPrefs.DeleteKey(_key);

            SetNewData(levelName);
        }

        public void ResetData()
        {

        }

        public void SaveData()
        {
            _file = JsonUtility.ToJson(PlayerProgress);
            PlayerPrefs.SetString(_key, _file);
        }

        public void UpdateData()
        {
            PlayerProgress.Money = _playerMoneyService.Money;
            PlayerProgress.Score = _playerScoreService.Score;
            PlayerProgress.Level = SceneManager.GetActiveScene().name;
        }

        private void SetNewData(string levelName)
        {
            PlayerProgress.Money = Constants.MinimumMoney;
            PlayerProgress.Score = Constants.MinimumScore;
            PlayerProgress.Level = levelName;

            SaveData();
        }
    }
}