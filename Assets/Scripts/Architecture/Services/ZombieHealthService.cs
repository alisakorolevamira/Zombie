using System;

namespace Scripts.Architecture.Services
{
    public class ZombieHealthService : IZombieHealthService
    {
        private readonly IZombieRewardService _rewardService;
        private readonly ISaveLoadService _saveLoadService;

        public ZombieHealthService(IZombieRewardService rewardService, ISaveLoadService saveLoadService)
        {
            _rewardService = rewardService;
            _saveLoadService = saveLoadService;
        }

        public event Action<int, int> HealthChanged;
        public event Action DamageApplied;
        public event Action Died;

        public int Health => _saveLoadService.ZombieProgress.Health;

        public void ChangeHealth(int damage)
        {
            _saveLoadService.ZombieProgress.Health -= damage;

            _rewardService.GiveRewardToPlayer();

            HealthChanged?.Invoke(_saveLoadService.ZombieProgress.Health, Constants.ZombieMaximumHealth);
            DamageApplied?.Invoke();

            CheckDeath();
            _saveLoadService.SaveProgress();
        }

        private void CheckDeath()
        {
            if (Health <= 0)
                Died?.Invoke();
        }
    }
}