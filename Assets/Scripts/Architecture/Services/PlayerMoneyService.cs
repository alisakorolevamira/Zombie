using Scripts.Constants;
using Scripts.Progress;
using System;

namespace Scripts.Architecture.Services
{
    public class PlayerMoneyService : IPlayerMoneyService
    {
        public event Action MoneyChanged;

        public int Money { get; private set; }

        public void Initialize(PlayerProgress playerProgress) => Money = playerProgress.Money;

        public bool IsEnoughMoney(int value)
        {
            return Money >= value;
        }

        public void AddMoney(int value)
        {
            if (value < PlayerConstants.MinimumMoney)
                return;

            Money += value;

            MoneyChanged?.Invoke();
        }

        public void SpendMoney(int value)
        {
            if (Money >= value)
            {
                Money -= value;

                MoneyChanged?.Invoke();
            }
        }
    }
}