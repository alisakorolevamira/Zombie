using System;

namespace Scripts.Architecture.Services
{
    public interface IPlayerMoneyService : IService
    {
        event Action MoneyChanged;

        int Money { get; }
        int AddReward { get; }

        bool IsEnoughMoney(int value);
        void AddMoney(int value);
        void SpendMoney(int value);
    }
}