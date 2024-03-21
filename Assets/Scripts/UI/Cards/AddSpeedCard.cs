using DG.Tweening;
using Scripts.Architecture.Services;
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

        public override void Open()
        {
            base.Open();

            _spawnerService = AllServices.Container.Single<ISpawnerService>();
            _citizenSpawner = _spawnerService.CitizenSpawner;
            _priceText.text = _saveLoadService.CardsPricesProgress.AddSpeedCardPrice.ToString();
        }

        private protected override void OnButtonClicked()
        {
            if (_playerMoneyService.Money >= _saveLoadService.CardsPricesProgress.AddSpeedCardPrice && _citizenSpawner.Citizens != null)
            {
                CardBought?.Invoke(_saveLoadService.CardsPricesProgress.AddSpeedCardPrice);

                foreach (var sitizen in _citizenSpawner.Citizens)
                    sitizen.AddSpeed();

                _saveLoadService.CardsPricesProgress.AddSpeedCardPrice *= _coefficientOfInceasing;
                _priceText.text = _saveLoadService.CardsPricesProgress.AddSpeedCardPrice.ToString();

                base.OnButtonClicked();
            }
        }

        private protected override void ChangeColor()
        {
            if (_playerMoneyService.Money >= _saveLoadService.CardsPricesProgress.AddSpeedCardPrice)
                _image.DOColor(Color.green, _timeOfChangingColor);

            else
                base.ChangeColor();
        }
    }
}
