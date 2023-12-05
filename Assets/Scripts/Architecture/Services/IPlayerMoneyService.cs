using UnityEngine.Events;

namespace Scripts.Architecture.Services
{
    public interface IPlayerMoneyService : IService
    {
        event UnityAction MoneyChanged;

        void AddMoney(int value);
        bool IsEnoughMoney(int value);
        int Money();
        void SpendMoney(int value);
    }
}