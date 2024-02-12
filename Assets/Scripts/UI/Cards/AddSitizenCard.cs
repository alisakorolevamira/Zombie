using DG.Tweening;
using Scripts.Architecture.Services;
using Scripts.Spawner;
using System;
using UnityEngine;

namespace Scripts.UI.Cards
{
    public class AddSitizenCard : Card
    {
        private ISpawnerService _spawnerService;
        private SitizenSpawner _sitizenSpawner;

        public event Action OnClicked;
        public override event Action<int> CardBought;

        public override void Close()
        {
            base.Close();

            _spawnerService = AllServices.Container.Single<ISpawnerService>();
            _sitizenSpawner = _spawnerService.CurrentSitizenSpawner;

            _sitizenSpawner.NumberOfSitizensChanged -= ChangeColor;
        }

        public override void Open()
        {
            base.Open();

            _priceText.text = _saveLoadService.CardsPricesProgress.AddSitizenCardPrice.ToString();

            _sitizenSpawner.NumberOfSitizensChanged += ChangeColor;

        }

        private protected override void OnButtonClicked()
        {
            if (_playerMoneyService.Money >= _saveLoadService.CardsPricesProgress.AddSitizenCardPrice && _sitizenSpawner.CheckAmountOfSitizens())
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

            if (_playerMoneyService.Money >= _saveLoadService.CardsPricesProgress.AddSitizenCardPrice && _sitizenSpawner.CheckAmountOfSitizens())
                _image.DOColor(Color.green, _timeOfChangingColor);

            else
                base.ChangeColor();
        }
    }
}
