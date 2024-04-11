using Progress;

namespace Architecture.ServicesInterfaces.Zombie
{
    public interface IZombieRewardService : IService
    {
        int MoneyReward { get; }
        int ScoreReward { get; }

        void Initialize(ZombieProgress zombieProgress);
        void DoubleReward();
        void GiveRewardToPlayer();
    }
}