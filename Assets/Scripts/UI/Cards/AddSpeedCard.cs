using DG.Tweening;
using Scripts.Architecture.Services;
using Scripts.Constants;
using Scripts.Spawner;
using System;
using UnityEngine;

namespace Scripts.UI.Cards
{
    public class AddSpeedCard : Card
    {
        private ISpawnerService _spawnerService;
        private CitizenSpawner _citizenSpawner;

        public override event Action<int> CardBought;

        public override void Close()
        {
            base.Close();

            _spawnerService = AllServices.Container.Single<ISpawnerService>();
            _citizenSpawner = _spawnerService.CitizenSpawner;
        }

        public override void Open()
        {
            base.Open();

            Price = _cardsPricesDataService.CardsPricesProgress.AddSpeedCardPrice;
            _priceText.text = Price.ToString();
        }

        private protected override void OnButtonClicked()
        {
            if (_playerMoneyService.Money >= Price && _citizenSpawner.Citizens != null)
            {
                CardBought?.Invoke(Price);

                foreach (var sitizen in _citizenSpawner.Citizens)
                    sitizen.AddSpeed();

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