using System;
using Progress;

namespace Architecture.ServicesInterfaces.Player
{
    public interface IPlayerMoneyService : IService
    {
        event Action MoneyChanged;

        int Money { get; }

        void Initialize(PlayerProgress playerProgress);
        void AddMoney(int value);
        void SpendMoney(int value);
    }
}