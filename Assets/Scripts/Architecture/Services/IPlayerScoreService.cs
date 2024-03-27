using Scripts.Progress;
using System;

namespace Scripts.Architecture.Services
{
    public interface IPlayerScoreService : IService
    {
        event Action ScoreChanged;

        int Score { get; }

        void Initialize(PlayerProgress playerProgress);
        void AddScore(int value);
    }
}