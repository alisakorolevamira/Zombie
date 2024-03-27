using Scripts.Progress;

namespace Scripts.Architecture.Services
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