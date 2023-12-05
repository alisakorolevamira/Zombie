namespace Scripts.Architecture.Services
{
    public interface IZombieRewardService : IService
    {
        int Reward { get; }

        void DoubleReward();
        void GiveRewardToPlayer();
    }
}