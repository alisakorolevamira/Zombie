using Architecture.ServicesInterfaces.Player;
using Architecture.ServicesInterfaces.Zombie;
using Constants.Characters;
using Progress;

namespace Architecture.Services.Zombie
{
    public class ZombieRewardService : IZombieRewardService
    {
        private readonly IPlayerMoneyService _playerMoneyService;
        private readonly IPlayerScoreService _playerScoreService;

        public ZombieRewardService(IPlayerMoneyService playerMoneyService, IPlayerScoreService playerScoreService)
        {
            _playerMoneyService = playerMoneyService;
            _playerScoreService = playerScoreService;
        }

        public int MoneyReward { get; private set; }
        public int ScoreReward { get; private set; }

        public void Initialize(ZombieProgress zombieProgress)
        {
            MoneyReward = zombieProgress.MoneyReward;
            ScoreReward = zombieProgress.ScoreReward;
        }

        public void DoubleReward()
        {
            MoneyReward *= ZombieConstants.CoefficientOfChangingReward;
            ScoreReward *= ZombieConstants.CoefficientOfChangingReward;
        }

        public void GiveRewardToPlayer()
        {
            _playerMoneyService.AddMoney(MoneyReward);
            _playerScoreService.AddScore(ScoreReward);
        }
    }
}