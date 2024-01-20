namespace Scripts.Architecture.Services
{
    public class ZombieRewardService : IZombieRewardService
    {
        private readonly int _coefficientOfChangingReward = 2;
        private readonly IPlayerMoneyService _playerMoneyService;
        private readonly IZombieProgressService _zombieProgressService;

        public int Reward { get { return _zombieProgressService.Progress.Reward; } }

        public ZombieRewardService(IPlayerMoneyService playerMoneyService, IZombieProgressService zombieProgressService)
        {
            _playerMoneyService = playerMoneyService;
            _zombieProgressService = zombieProgressService;
        }

        public void DoubleReward()
        {
            _zombieProgressService.Progress.Reward *= _coefficientOfChangingReward;
        }

        public void GiveRewardToPlayer()
        {
            _playerMoneyService.AddMoney(_zombieProgressService.Progress.Reward);
        }
    }
}
