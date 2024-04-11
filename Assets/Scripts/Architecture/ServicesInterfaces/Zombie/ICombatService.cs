using System;

namespace Architecture.ServicesInterfaces.Zombie
{
    public interface ICombatService : IService, IDisposable
    {
        event Action AllCitizensDied;
        int CitizenCount { get; }

        void Initialize();
        void ApplyDamage(int damage);
        void AllCitizensDie();
    }
}