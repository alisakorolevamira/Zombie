using Scripts.Progress;

namespace Scripts.Architecture.Services
{
    public class ZombieProgressService : IZombieProgressService
    {
        private const string ZombiesHealth = "ZombiesHealth";
        private readonly int _maximumHealth = 500;
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
            Progress = new ZombieProgress(health);
        }

        public void SaveProgress()
        {
            _saveLoadService.SaveProgress(ZombiesHealth, Progress.Health);
        }

        public void ResetProgress()
        {
            _saveLoadService.ResetProgress(ZombiesHealth);
        }
    }
}
