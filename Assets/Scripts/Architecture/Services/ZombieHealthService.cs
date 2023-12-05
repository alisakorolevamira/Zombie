using UnityEngine.Events;

namespace Scripts.Architecture.Services
{
    public class ZombieHealthService : IZombieHealthService
    {
        private readonly IZombieProgressService _progressService;
        private readonly IZombieRewardService _rewardService;

        public event UnityAction<int, int> HealthChanged;
        public event UnityAction DamageApplied;
        public event UnityAction Died;

        public ZombieHealthService(IZombieProgressService progressService, IZombieRewardService rewardService)
        {
            _progressService = progressService;
            _rewardService = rewardService;
        }

        public int Health()
        {
            return _progressService.Progress.Health;
        }

        public void ChangeHealth(int damage)
        {
            _progressService.Progress.Health -= damage;

            _rewardService.GiveRewardToPlayer();

            HealthChanged?.Invoke(_progressService.Progress.Health, _progressService.MaximumHealth);
            DamageApplied?.Invoke();

            CheckDeath();
            _progressService.SaveProgress();
        }

        private void CheckDeath()
        {
            if (Health() <= 0)
            {
                Died?.Invoke();
            }
        }
    }
}