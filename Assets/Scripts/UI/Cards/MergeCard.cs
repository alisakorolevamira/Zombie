using DG.Tweening;
using Scripts.Architecture.Services;
using Scripts.Characters.Citizens;
using Scripts.Constants;
using Scripts.Spawner;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.UI.Cards
{
    public class MergeCard : Card
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

            Price = _cardsPricesDataService.CardsPricesProgress.MergeCardPrice;
            _priceText.text = Price.ToString();

            _citizenSpawner.NumberOfCitizensChanged += ChangeColor;
        }

        private protected override void OnButtonClicked()
        {
            if (_playerMoneyService.Money >= Price && _citizenSpawner.Citizens.Count >= CardsConstants.RequiredNumberOfCitizensForMerge)
            {
                OnClicked?.Invoke();
                CardBought?.Invoke(Price);

                Price *= CardsConstants.CoefficientOfInceasingPrice;
                _priceText.text = Price.ToString();

                base.OnButtonClicked();
            }
        }

        private protected override void ChangeColor()
        {
            List<Citizen> firstLevelCitizens = _citizenSpawner.Citizens.FindAll(p => p.GetComponent<Citizen>().TypeId == CitizenTypeId.FirstCitizen);

            if (_playerMoneyService.Money >= Price && firstLevelCitizens.Count >= CardsConstants.RequiredNumberOfCitizensForMerge)
                _image.DOColor(Color.green, CardsConstants.TimeOfChangingColor);

            else
                base.ChangeColor();
        }
    }
}