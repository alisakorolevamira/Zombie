namespace Scripts.Architecture.Services
{
    public class ZombieRewardService : IZombieRewardService
    {
        private readonly int _coefficientOfChangingReward = 2;
        private readonly IPlayerMoneyService _playerMoneyService;
        private readonly IPlayerScoreService _playerScoreService;
        private readonly ISaveLoadService _saveLoadService;

        public ZombieRewardService(IPlayerMoneyService playerMoneyService, IPlayerScoreService playerScoreService,
            ISaveLoadService saveLoadService)
        {
            _playerMoneyService = playerMoneyService;
            _saveLoadService = saveLoadService;
            _playerScoreService = playerScoreService;
        }

        public void DoubleReward()
        {
            _saveLoadService.ZombieProgress.MoneyReward *= _coefficientOfChangingReward;
            _saveLoadService.ZombieProgress.ScoreReward *= _coefficientOfChangingReward;
        }

        public void GiveRewardToPlayer()
        {
            _playerMoneyService.AddMoney(_saveLoadService.ZombieProgress.MoneyReward);
            _playerScoreService.AddScore(_saveLoadService.ZombieProgress.ScoreReward);
        }
    }
}
