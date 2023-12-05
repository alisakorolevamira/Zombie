using Scripts.Architecture.Services;
using UnityEngine.Events;

namespace Scripts.UI.Cards
{
    public class DoubleRewardCard : Card
    {
        private IZombieRewardService _zombieRewardService;

        public override event UnityAction<int> CardBought;

        private protected override void OnButtonClicked()
        {
            if (_playerMoneyService.Money() >= _currentPrice)
            {
                CardBought?.Invoke(_currentPrice);

                _zombieRewardService = AllServices.Container.Single<IZombieRewardService>();
                _zombieRewardService.DoubleReward();

                base.OnButtonClicked();
            }
        }
    }
}
