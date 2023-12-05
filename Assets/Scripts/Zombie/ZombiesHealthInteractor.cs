using UnityEngine;
using UnityEngine.Events;

public class ZombiesHealthInteractor : Interactor
{
    public event UnityAction<int, int> HealthChanged;
    public event UnityAction DamageApplied;
    public event UnityAction Died;

    public int Health()
    {
        var zombiesHealthRepository = Game.GetRepository<ZombiesHealthRepository>();
        return zombiesHealthRepository.Health;
    }

    public void ChangeHealth(int damage)
    {
        var repository = Game.GetRepository<ZombiesHealthRepository>();
        var interactor = Game.GetInteractor<ZombiesRewardInteractor>();

        repository.Health -= damage;
        interactor.GiveRewardToPlayer();
        HealthChanged?.Invoke(repository.Health, repository.MaximumHealth);
        DamageApplied?.Invoke();
        CheckDeath();
        repository.Save();
    }

    private void CheckDeath()
    {
        if (Health() <= 0)
        {
            Died?.Invoke();
        }
    }
}
