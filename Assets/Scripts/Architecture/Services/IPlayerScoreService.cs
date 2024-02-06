using System;

namespace Scripts.Architecture.Services
{
    public interface IPlayerScoreService : IService
    {
        event Action ScoreChanged;

        int Score { get; }

        void AddScore(int value);
        void RemoveScore();
    }
}