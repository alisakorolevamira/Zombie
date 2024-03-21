namespace Scripts.Architecture.Services
{
    public class StarCountService : IStarCountService
    {
        private const int MinimumRateAmountOfStars = 1;
        private const int MediumRateAmountOfStars = 2;
        private const int MaximumRateAmountOfStars = 3;

        private readonly IPlayerScoreService _playerScoreService;

        private int _stars;

        public StarCountService(IPlayerScoreService playerScoreService)
        {
            _playerScoreService = playerScoreService;
        }

        public int CountStars(int meduimScore, int highScore)
        {
            if (_playerScoreService.Score >= highScore)
                _stars = MaximumRateAmountOfStars;

            else if (_playerScoreService.Score >= meduimScore && _playerScoreService.Score < highScore)
                _stars = MediumRateAmountOfStars;

            else
                _stars = MinimumRateAmountOfStars;

            return _stars;
        }
    }
}
