using System;

namespace Scripts.Architecture.Services
{
    public interface IZombieHealthService : IService
    {
        event Action DamageApplied;
        event Action Died;
        event Action<int, int> HealthChanged;

        int Health { get; }

        void ChangeHealth(int damage);
    }
}