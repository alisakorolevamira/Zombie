using DG.Tweening;
using Scripts.Architecture.Services;
using System;
using UnityEngine;

namespace Scripts.UI.Cards
{
    public class DoubleRewardCard : Card
    {
        private IZombieRewardService _zombieRewardService;

        public override event Action<int> CardBought;

        public override void Open()
        {
            base.Open();

            _priceText.text = _saveLoadService.CardsPricesProgress.DoubleRewardCardPrice.ToString();
            _zombieRewardService = AllServices.Container.Single<IZombieRewardService>();
        }

        private protected override void OnButtonClicked()
        {
            if (_playerMoneyService.Money >= _saveLoadService.CardsPricesProgress.DoubleRewardCardPrice)
            {
                CardBought?.Invoke(_saveLoadService.CardsPricesProgress.DoubleRewardCardPrice);

                _zombieRewardService.DoubleReward();
                _saveLoadService.CardsPricesProgress.DoubleRewardCardPrice *= _coefficientOfInceasing;
                _priceText.text = _saveLoadService.CardsPricesProgress.DoubleRewardCardPrice.ToString();

                base.OnButtonClicked();
            }
        }

        private protected override void ChangeColor()
        {
            if (_playerMoneyService.Money >= _saveLoadService.CardsPricesProgress.DoubleRewardCardPrice)
                _image.DOColor(Color.green, _timeOfChangingColor);

            else
                base.ChangeColor();
        }
    }
}
