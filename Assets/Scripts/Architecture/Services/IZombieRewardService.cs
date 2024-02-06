namespace Scripts.Architecture.Services
{
    public interface IZombieRewardService : IService
    {
        void DoubleReward();
        void GiveRewardToPlayer();
    }
}