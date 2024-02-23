using System;

namespace Scripts.Architecture.Services
{
    public class PlayerMoneyService : IPlayerMoneyService
    {
        private readonly ISaveLoadService _saveLoadService;

        public PlayerMoneyService(ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
        }

        public event Action MoneyChanged;

        public int Money => _saveLoadService.PlayerProgress.Money;

        public bool IsEnoughMoney(int value)
        {
            return _saveLoadService.PlayerProgress.Money >= value;
        }

        public void AddMoney(int value)
        {
            _saveLoadService.PlayerProgress.Money += value;

            MoneyChanged?.Invoke();
        }

        public void SpendMoney(int value)
        {
            if (_saveLoadService.PlayerProgress.Money >= value)
            {
                _saveLoadService.PlayerProgress.Money -= value;

                MoneyChanged?.Invoke();
            }
        }
    }
}
