using DG.Tweening;
using Scripts.Architecture.Services;
using Scripts.Constants;
using System;
using UnityEngine;

namespace Scripts.UI.Cards
{
    public class DoubleRewardCard : Card
    {
        private IZombieRewardService _zombieRewardService;

        public override event Action<int> CardBought;

        public override void Close()
        {
            base.Close();

            _zombieRewardService = AllServices.Container.Single<IZombieRewardService>();
        }

        public override void Open()
        {
            base.Open();

            Price = _cardsPricesDataService.CardsPricesProgress.DoubleRewardCardPrice;
            _priceText.text = Price.ToString();
        }

        private protected override void OnButtonClicked()
        {
            if (_playerMoneyService.Money >= Price)
            {
                CardBought?.Invoke(Price);

                _zombieRewardService.DoubleReward();
                Price *= CardsConstants.CoefficientOfInceasingPrice;
                _priceText.text = Price.ToString();

                base.OnButtonClicked();
            }
        }

        private protected override void ChangeColor()
        {
            if (_playerMoneyService.Money >= Price)
                _image.DOColor(Color.green, CardsConstants.TimeOfChangingColor);

            else
                base.ChangeColor();
        }
    }
}