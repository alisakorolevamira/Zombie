using UnityEngine.Events;

namespace Scripts.Architecture.Services
{
    public interface IZombieHealthService : IService
    {
        event UnityAction DamageApplied;
        event UnityAction Died;
        event UnityAction<int, int> HealthChanged;

        void ChangeHealth(int damage);
        int Health();
    }
}