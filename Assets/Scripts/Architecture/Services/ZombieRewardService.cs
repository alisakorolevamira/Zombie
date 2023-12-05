namespace Scripts.Architecture.Services
{
    public class ZombieRewardService : IZombieRewardService
    {
        private readonly int _coefficientOfChangingReward = 2;
        private readonly IPlayerMoneyService _playerMoneyService;
        private int _reward = 10;

        public ZombieRewardService(IPlayerMoneyService playerMoneyService)
        {
            _playerMoneyService = playerMoneyService;
        }

        public int Reward { get { return _reward; } }

        public void DoubleReward()
        {
            _reward *= _coefficientOfChangingReward;
        }

        public void GiveRewardToPlayer()
        {
            _playerMoneyService.AddMoney(_reward);
        }
    }
}
