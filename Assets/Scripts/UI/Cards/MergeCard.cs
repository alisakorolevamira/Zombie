using DG.Tweening;
using Scripts.Architecture.Services;
using Scripts.Characters.Citizens;
using Scripts.Spawner;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.UI.Cards
{
    public class MergeCard : Card
    {
        private readonly int _requiredNumberOfCitizens = 2;

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

            _priceText.text = _saveLoadService.CardsPricesProgress.MergeCardPrice.ToString();

            _citizenSpawner.NumberOfCitizensChanged += ChangeColor;
        }

        private protected override void OnButtonClicked()
        {
            if (_playerMoneyService.Money >= _saveLoadService.CardsPricesProgress.MergeCardPrice && _citizenSpawner.Citizens.Count >= _requiredNumberOfCitizens)
            {
                OnClicked?.Invoke();
                CardBought?.Invoke(_saveLoadService.CardsPricesProgress.MergeCardPrice);

                _saveLoadService.CardsPricesProgress.MergeCardPrice *= _coefficientOfInceasing;
                _priceText.text = _saveLoadService.CardsPricesProgress.MergeCardPrice.ToString();

                base.OnButtonClicked();
            }
        }

        private protected override void ChangeColor()
        {
            List<Citizen> firstLevelCitizens = _citizenSpawner.Citizens.FindAll(p => p.GetComponent<Citizen>().TypeId == CitizenTypeId.FirstCitizen);

            if (_playerMoneyService.Money >= _saveLoadService.CardsPricesProgress.MergeCardPrice && firstLevelCitizens.Count >= _requiredNumberOfCitizens)
                _image.DOColor(Color.green, _timeOfChangingColor);

            else
                base.ChangeColor();
        }
    }
}
