using System;

namespace Scripts.Architecture.Services
{
    public class PlayerScoreService : IPlayerScoreService
    {
        private readonly ISaveLoadService _saveLoadService;

        public PlayerScoreService(ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
        }

        public event Action ScoreChanged;

        public int Score => _saveLoadService.PlayerProgress.Score;

        public void AddScore(int value)
        {
            _saveLoadService.PlayerProgress.Score += value;

            ScoreChanged?.Invoke();
        }

        public void RemoveScore()
        {
            if (_saveLoadService.PlayerProgress.Score >= Constants.LoseScoreFine)
                _saveLoadService.PlayerProgress.Score -= Constants.LoseScoreFine;

            else
                _saveLoadService.PlayerProgress.Score = Constants.MinimumScore;

            ScoreChanged?.Invoke();
        }
    }
}
