using UnityEngine.Events;

namespace Scripts.Architecture.Services
{
    public class PlayerMoneyService : IPlayerMoneyService
    {
        private readonly IPlayerProgressService _playerProgressService;

        public event UnityAction MoneyChanged;

        public PlayerMoneyService(IPlayerProgressService progressService)
        {
            _playerProgressService = progressService;
        }

        public int Money()
        {
            return _playerProgressService.Progress.Money;
        }

        public bool IsEnoughMoney(int value)
        {
            return _playerProgressService.Progress.Money >= value;
        }

        public void AddMoney(int value)
        {
            _playerProgressService.Progress.Money += value;

            MoneyChanged?.Invoke();
            _playerProgressService.SaveProgress();
        }

        public void SpendMoney(int value)
        {
            if (_playerProgressService.Progress.Money >= value)
            {
                _playerProgressService.Progress.Money -= value;

                MoneyChanged?.Invoke();
                _playerProgressService.SaveProgress();
            }
        }
    }
}
