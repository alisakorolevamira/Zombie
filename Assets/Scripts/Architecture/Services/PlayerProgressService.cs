using Scripts.Progress;
using UnityEngine.SceneManagement;

namespace Scripts.Architecture.Services
{
    public class PlayerProgressService : IPlayerProgressService
    {
        private const string Money = "Money";
        private const string Level = "Level";
        private readonly int _menuIndex = 1;
        private readonly int _defaultValue = 0;

        private readonly ISaveLoadService _saveLoadService;

        public PlayerProgress Progress { get; private set; }

        public PlayerProgressService(ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
        }

        public void LoadProgress()
        {
            int money = _saveLoadService.LoadProgress(Money, _defaultValue);
            int level = _saveLoadService.LoadProgress(Level, _menuIndex);

            Progress = new PlayerProgress(money, level);
        }

        public void SaveProgress()
        {
            UpdateProgress();

            _saveLoadService.SaveProgress(Money, Progress.Money);
            _saveLoadService.SaveProgress(Level, Progress.Level);
        }

        public void ResetProgress()
        {
            _saveLoadService.ResetProgress(Money);
            _saveLoadService.ResetProgress(Level);
        }

        private void UpdateProgress()
        {
            Progress.Level = SceneManager.GetActiveScene().buildIndex;
        }
    }
}
