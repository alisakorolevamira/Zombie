using Architecture.ServicesInterfaces.Player;
using Architecture.ServicesInterfaces.UI;
using Constants.UI;

namespace Architecture.Services.UI
{
    public class StarCountService : IStarCountService
    {
        private readonly IPlayerScoreService _playerScoreService;

        private int _stars;

        public StarCountService(IPlayerScoreService playerScoreService)
        {
            _playerScoreService = playerScoreService;
        }

        public int CountStars(int mediumScore, int highScore)
        {
            if (_playerScoreService.Score >= highScore)
                _stars = StarsConstants.MaximumRateAmountOfStars;
            else if (_playerScoreService.Score >= mediumScore && _playerScoreService.Score < highScore)
                _stars = StarsConstants.MediumRateAmountOfStars;
            else
                _stars = StarsConstants.MinimumRateAmountOfStars;

            return _stars;
        }
    }
}