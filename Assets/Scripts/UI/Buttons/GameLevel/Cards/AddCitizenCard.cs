using System;
using Architecture.Services;
using Architecture.ServicesInterfaces;
using Constants.UI;
using DG.Tweening;
using Spawner;
using UnityEngine;

namespace UI.Buttons.GameLevel.Cards
{
    public class AddCitizenCard : Card
    {
        private ISpawnerService _spawnerService;
        private CitizenSpawner _citizenSpawner;

        public event Action OnClicked;
        public override event Action<int> CardBought;

        public override void Close()
        {
            base.Close();

            _spawnerService = AllServices.Container.Single<ISpawnerService>();
            _citizenSpawner = _spawnerService.CitizenSpawner;

            _citizenSpawner.NumberOfCitizensChanged -= ChangeColor;
        }

        public override void Open()
        {
            base.Open();

            Price = _cardsPricesDataService.CardsPricesProgress.AddCitizenCardPrice;
            _priceText.text = Price.ToString();

            _citizenSpawner.NumberOfCitizensChanged += ChangeColor;
        }

        private protected override void OnButtonClicked()
        {
            if (_playerMoneyService.Money >= Price && _citizenSpawner.CheckAmountOfCitizens())
            {
                OnClicked?.Invoke();
                CardBought?.Invoke(Price);

                Price *= CardsConstants.CoefficientOfIncreasingPrice;
                _priceText.text = Price.ToString();

                base.OnButtonClicked();
            }
        }

        private protected override void ChangeColor()
        {
            if (_playerMoneyService.Money >= Price && _citizenSpawner.CheckAmountOfCitizens())
                _image.DOColor(Color.green, CardsConstants.TimeOfChangingColor);
            else
                base.ChangeColor();
        }
    }
}