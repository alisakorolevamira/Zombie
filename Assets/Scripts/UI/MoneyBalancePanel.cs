using Scripts.Architecture.Services;
using TMPro;

namespace Scripts.UI
{
    public class MoneyBalancePanel : Panel
    {
        private TMP_Text _money;
        private IPlayerMoneyService _playerMoneyService;

        private void OnEnable()
        {
            _money = GetComponentInChildren<TMP_Text>();
            _playerMoneyService = AllServices.Container.Single<IPlayerMoneyService>();
            _money.text = _playerMoneyService.Money().ToString();

            _playerMoneyService.MoneyChanged += OnMoneyChanged;

            Open();
        }

        private void OnDisable()
        {
            _playerMoneyService.MoneyChanged -= OnMoneyChanged;
        }

        private void OnMoneyChanged()
        {
            _money.text = _playerMoneyService.Money().ToString();
        }
    }
}
