using System;
using Progress;

namespace Architecture.ServicesInterfaces.Player
{
    public interface IPlayerScoreService : IService
    {
        event Action ScoreChanged;

        int Score { get; }

        void Initialize(PlayerProgress playerProgress);
        void AddScore(int value);
    }
}