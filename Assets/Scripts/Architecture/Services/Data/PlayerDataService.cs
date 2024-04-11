using Architecture.ServicesInterfaces.Data;
using Architecture.ServicesInterfaces.Player;
using Constants;
using Constants.Characters;
using Progress;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Architecture.Services.Data
{
    public class PlayerDataService : IPlayerDataService
    {
        private readonly IPlayerMoneyService _playerMoneyService;
        private readonly IPlayerScoreService _playerScoreService;

        private string _file;

        public PlayerDataService(IPlayerMoneyService playerMoneyService, IPlayerScoreService playerScoreService)
        {
            _playerMoneyService = playerMoneyService;
            _playerScoreService = playerScoreService;
        }

        public PlayerProgress PlayerProgress { get; private set; } = new();

        public void LoadData()
        {
            _file = PlayerPrefs.GetString(PlayerConstants.Key, string.Empty);

            if (_file == string.Empty)
                SetNewData(LevelConstants.Menu);

            else
                PlayerProgress = JsonUtility.FromJson<PlayerProgress>(_file);

            _playerMoneyService.Initialize(PlayerProgress);
            _playerScoreService.Initialize(PlayerProgress);
        }

        public void ResetData(string levelName)
        {
            PlayerPrefs.DeleteKey(PlayerConstants.Key);

            SetNewData(levelName);
        }

        public void SaveData()
        {
            _file = JsonUtility.ToJson(PlayerProgress);
            PlayerPrefs.SetString(PlayerConstants.Key, _file);
        }

        public void UpdateData()
        {
            PlayerProgress.Money = _playerMoneyService.Money;
            PlayerProgress.Score = _playerScoreService.Score;
            PlayerProgress.Level = SceneManager.GetActiveScene().name;
        }

        private void SetNewData(string levelName)
        {
            PlayerProgress.Money = PlayerConstants.MinimumMoney;
            PlayerProgress.Score = PlayerConstants.MinimumScore;
            PlayerProgress.Level = levelName;

            SaveData();
        }
    }
}