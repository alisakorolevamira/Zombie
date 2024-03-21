using DG.Tweening;
using Scripts.Architecture.Services;
using Scripts.Spawner;
using System;
using UnityEngine;

namespace Scripts.UI.Cards
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

            _priceText.text = _saveLoadService.CardsPricesProgress.AddSitizenCardPrice.ToString();

            _citizenSpawner.NumberOfCitizensChanged += ChangeColor;

        }

        private protected override void OnButtonClicked()
        {
            if (_playerMoneyService.Money >= _saveLoadService.CardsPricesProgress.AddSitizenCardPrice && _citizenSpawner.CheckAmountOfCitizens())
            {
                OnClicked?.Invoke();
                CardBought?.Invoke(_saveLoadService.CardsPricesProgress.AddSitizenCardPrice);

                _saveLoadService.CardsPricesProgress.AddSitizenCardPrice *= _coefficientOfInceasing;
                _priceText.text = _saveLoadService.CardsPricesProgress.AddSitizenCardPrice.ToString();

                base.OnButtonClicked();
            }
        }

        private protected override void ChangeColor()
        {

            if (_playerMoneyService.Money >= _saveLoadService.CardsPricesProgress.AddSitizenCardPrice && _citizenSpawner.CheckAmountOfCitizens())
                _image.DOColor(Color.green, _timeOfChangingColor);

            else
                base.ChangeColor();
        }
    }
}
