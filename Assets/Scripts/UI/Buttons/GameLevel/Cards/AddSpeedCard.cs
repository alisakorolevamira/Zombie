using System;
using Architecture.Services;
using Architecture.ServicesInterfaces;
using Characters.GameLevel.Citizens;
using Constants.UI;
using DG.Tweening;
using Spawner;
using UnityEngine;

namespace UI.Buttons.GameLevel.Cards
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

                foreach (Citizen citizen in _citizenSpawner.Citizens)
                    citizen.AddSpeed();

                Price *= CardsConstants.CoefficientOfIncreasingPrice;
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