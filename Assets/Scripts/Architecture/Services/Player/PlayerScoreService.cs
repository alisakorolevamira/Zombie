using System;
using Architecture.ServicesInterfaces.Player;
using Constants.Characters;
using Progress;

namespace Architecture.Services.Player
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