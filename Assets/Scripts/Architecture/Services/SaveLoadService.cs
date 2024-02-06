using Agava.YandexGames;
using Scripts.Progress;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Architecture.Services
{
    public class SaveLoadService : ISaveLoadService
    {
        private ProgressData _data;
        private string _file;

        public PlayerProgress PlayerProgress { get; private set; }
        public ZombieProgress ZombieProgress { get; private set; }
        public CardsPricesProgress CardsPricesProgress { get; private set; }

        public void LoadProgress()
        {
            PlayerAccount.GetCloudSaveData((data) => _file = data);

            _data = JsonUtility.FromJson<ProgressData>(_file);

            if (_data == null)
                _data = new();

            UpdateProgress();
        }

        public void SaveProgress()
        {
            UpdateData();

            _file = JsonUtility.ToJson(_data);

            PlayerAccount.SetCloudSaveData(_file);
        }

        public void ResetProgress()
        {
            _data = new();
            _data.PlayerScore = PlayerProgress.Score;

            UpdateProgress();
        }

        private void UpdateData()
        {
            PlayerProgress.Level = SceneManager.GetActiveScene().buildIndex;

            _data.Update(PlayerProgress, ZombieProgress, CardsPricesProgress);
        }

        private void UpdateProgress()
        {
            PlayerProgress = new(_data.PlayerLevel, _data.PlayerMoney, _data.PlayerScore);
            ZombieProgress = new(_data.ZombieHealth, _data.ZombieMoneyReward, _data.ZombieScoreReward);
            CardsPricesProgress = new(_data.AddSitizenCardPrice, _data.MergeCardPrice, _data.AddSpeedCardPrice, _data.DoubleRewardCardPrice);
        }
    }
}