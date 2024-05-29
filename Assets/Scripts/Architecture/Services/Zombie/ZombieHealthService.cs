using System;
using Architecture.ServicesInterfaces.Zombie;
using Constants.Characters;
using Progress;

namespace Architecture.Services.Zombie
{
    public class ZombieHealthService : IZombieHealthService
    {
        private readonly IZombieRewardService _rewardService;

        public ZombieHealthService(IZombieRewardService rewardService)
        {
            _rewardService = rewardService;
        }

        public event Action<int> HealthChanged;
        public event Action DamageApplied;
        public event Action Died;

        public int Health { get; private set; }

        public void Initialize(ZombieProgress zombieProgress) => Health = zombieProgress.Health;

        public void TakeDamage(int damage)
        {
            if (Health >= damage)
                Health -= damage;
            else
                Health = ZombieConstants.ZombieMinimumHealth;

            _rewardService.GiveRewardToPlayer();

            HealthChanged?.Invoke(Health);
            DamageApplied?.Invoke();

            CheckDeath();
        }

        private void CheckDeath()
        {
            if (Health == ZombieConstants.ZombieMinimumHealth)
                Died?.Invoke();
        }
    }
}