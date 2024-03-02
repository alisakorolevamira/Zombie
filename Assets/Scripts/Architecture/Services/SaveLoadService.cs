using Agava.YandexGames;
using Cysharp.Threading.Tasks;
using Scripts.Progress;
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

        public async UniTask LoadProgress()
        {
            var task = new UniTaskCompletionSource<string>();
            
            PlayerAccount.GetCloudSaveData((data) =>
            {
                task.TrySetResult(data);
            }, 
            (error) => 
            { 
                task.TrySetException(null); 
            });
            
            _file = await task.Task;
            
            if (_file != null)
                _data = JsonUtility.FromJson<ProgressData>(_file);

            if (_data == null)
                _data = new();

            UpdateProgress();
        }

        public async UniTask SaveProgress()
        {
            UpdateData();
           
            _file = JsonUtility.ToJson(_data);
            
            await SetCloudSaveDataAsync(_file);
        }

        public void ResetProgress()
        {
            _data = new();
            _data.PlayerScore = PlayerProgress.Score;

            UpdateProgress();
        }

        private void UpdateData()
        {
            PlayerProgress.Level = SceneManager.GetActiveScene().name;

            _data.Update(PlayerProgress, ZombieProgress, CardsPricesProgress);
        }

        private void UpdateProgress()
        {
            PlayerProgress = new(_data.PlayerLevel, _data.PlayerMoney, _data.PlayerScore);
            ZombieProgress = new(_data.ZombieHealth, _data.ZombieMoneyReward, _data.ZombieScoreReward);
            CardsPricesProgress = new(_data.AddSitizenCardPrice, _data.MergeCardPrice, _data.AddSpeedCardPrice, _data.DoubleRewardCardPrice);
        }

        private async UniTask SetCloudSaveDataAsync(string file)
        {
            var task = new UniTaskCompletionSource();
            
            PlayerAccount.SetCloudSaveData(file, () => { task.TrySetResult(); });
            
            await task.Task;
        }
    }
}