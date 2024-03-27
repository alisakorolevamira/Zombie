using Scripts.Progress;
using System;

namespace Scripts.Architecture.Services
{
    public interface IPlayerMoneyService : IService
    {
        event Action MoneyChanged;

        int Money { get; }

        void Initialize(PlayerProgress playerProgress);
        bool IsEnoughMoney(int value);
        void AddMoney(int value);
        void SpendMoney(int value);
    }
}