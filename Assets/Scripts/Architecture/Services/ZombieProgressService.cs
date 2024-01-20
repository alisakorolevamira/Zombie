using Scripts.Progress;

namespace Scripts.Architecture.Services
{
    public class ZombieProgressService : IZombieProgressService
    {
        private const string ZombiesHealth = "ZombiesHealth";
        private const string ZombiesReward = "ZombiesReward";
        private readonly int _maximumHealth = 500;
        private readonly int _defaultReward = 10;
        private readonly ISaveLoadService _saveLoadService;

        public int MaximumHealth => _maximumHealth;

        public ZombieProgress Progress { get; private set; }

        public ZombieProgressService(ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
        }

        public void LoadProgress()
        {
            int health = _saveLoadService.LoadProgress(ZombiesHealth, MaximumHealth);
            int reward = _saveLoadService.LoadProgress(ZombiesReward, _defaultReward);

            Progress = new ZombieProgress(health, reward);
        }

        public void SaveProgress()
        {
            _saveLoadService.SaveProgress(ZombiesHealth, Progress.Health);
            _saveLoadService.SaveProgress(ZombiesReward, Progress.Reward);
        }

        public void ResetProgress()
        {
            _saveLoadService.ResetProgress(ZombiesHealth);
            _saveLoadService.ResetProgress(ZombiesReward);
        }
    }
}
