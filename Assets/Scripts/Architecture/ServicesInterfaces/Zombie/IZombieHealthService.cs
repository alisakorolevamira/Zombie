using System;
using Progress;

namespace Architecture.ServicesInterfaces.Zombie
{
    public interface IZombieHealthService : IService
    {
        event Action DamageApplied;
        event Action Died;
        event Action<int> HealthChanged;

        int Health { get; }

        void Initialize(ZombieProgress zombieProgress);
        void TakeDamage(int damage);
    }
}