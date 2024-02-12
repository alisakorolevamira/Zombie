using Scripts.Architecture.Services;
using TMPro;
using UnityEngine;

namespace Scripts.UI
{
    public class MoneyBalanceWindow : MonoBehaviour
    {
        [SerializeField] private TMP_Text _money;

        private IPlayerMoneyService _playerMoneyService;

        private void OnDisable()
        {
            _playerMoneyService.MoneyChanged -= OnMoneyChanged;
        }

        private void Start()
        {
            _playerMoneyService = AllServices.Container.Single<IPlayerMoneyService>();
            OnMoneyChanged();

            _playerMoneyService.MoneyChanged += OnMoneyChanged;
        }

        private void OnMoneyChanged()
        {
            _money.text = _playerMoneyService.Money.ToString();
        }
    }
}
