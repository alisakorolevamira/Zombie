using Scripts.Progress;
using System;

namespace Scripts.Architecture.Services
{
    public class ZombieHealthService : IZombieHealthService
    {
        private readonly IZombieRewardService _rewardService;

        public ZombieHealthService(IZombieRewardService rewardService)
        {
            _rewardService = rewardService;
        }

        public event Action<int, int> HealthChanged;
        public event Action DamageApplied;
        public event Action Died;

        public int Health { get; private set; }

        public void Initialize(ZombieProgress zombieProgress) => Health = zombieProgress.Health;

        public void ChangeHealth(int damage)
        {
            if (Health >= damage)
                Health -= damage;

            else
                Health = Constants.ZombieMinimumHealth;

            _rewardService.GiveRewardToPlayer();

            HealthChanged?.Invoke(Health, Constants.ZombieMaximumHealth);
            DamageApplied?.Invoke();

            CheckDeath();
        }

        private void CheckDeath()
        {
            if (Health == Constants.ZombieMinimumHealth)
                Died?.Invoke();
        }
    }
}