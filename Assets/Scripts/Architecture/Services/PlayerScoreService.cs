using Scripts.Constants;
using Scripts.Progress;
using System;

namespace Scripts.Architecture.Services
{
    public class PlayerScoreService : IPlayerScoreService
    {
        public event Action ScoreChanged;

        public int Score { get; private set; }

        public void Initialize(PlayerProgress playerProgress) => Score = playerProgress.Score;

        public void AddScore(int value)
        {
            if (value < PlayerConstants.MinimumScore)
                return;

            Score += value;

            ScoreChanged?.Invoke();
        }
    }
}