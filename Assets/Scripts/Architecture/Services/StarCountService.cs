using Scripts.Constants;

namespace Scripts.Architecture.Services
{
    public class StarCountService : IStarCountService
    {
        private readonly IPlayerScoreService _playerScoreService;

        private int _stars;

        public StarCountService(IPlayerScoreService playerScoreService)
        {
            _playerScoreService = playerScoreService;
        }

        public int CountStars(int meduimScore, int highScore)
        {
            if (_playerScoreService.Score >= highScore)
                _stars = StarsConstants.MaximumRateAmountOfStars;

            else if (_playerScoreService.Score >= meduimScore && _playerScoreService.Score < highScore)
                _stars = StarsConstants.MediumRateAmountOfStars;

            else
                _stars = StarsConstants.MinimumRateAmountOfStars;

            return _stars;
        }
    }
}