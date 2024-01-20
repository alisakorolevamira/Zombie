using DG.Tweening;
using Scripts.Architecture.Services;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts.UI.Cards
{
    public class DoubleRewardCard : Card
    {
        private IZombieRewardService _zombieRewardService;

        public override event UnityAction<int> CardBought;

        private protected override void OnEnable()
        {
            base.OnEnable();

            _priceText.text = _progressService.Progress.DoubleRewardCardPrice.ToString();
        }

        private protected override void OnButtonClicked()
        {
            if (_playerMoneyService.Money() >= _progressService.Progress.DoubleRewardCardPrice)
            {
                CardBought?.Invoke(_progressService.Progress.DoubleRewardCardPrice);

                _zombieRewardService = AllServices.Container.Single<IZombieRewardService>();
                _zombieRewardService.DoubleReward();
                _progressService.Progress.DoubleRewardCardPrice *= _coefficientOfInceasing;
                _priceText.text = _progressService.Progress.DoubleRewardCardPrice.ToString();

                base.OnButtonClicked();
            }
        }

        private protected override void ChangeColor()
        {
            if (_playerMoneyService.Money() >= _progressService.Progress.DoubleRewardCardPrice)
            {
                _image.DOColor(Color.green, _timeOfChangingColor);
            }

            else
            {
                base.ChangeColor();
            }
        }
    }
}
