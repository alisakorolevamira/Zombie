using DG.Tweening;
using Scripts.Architecture.Services;
using Scripts.Characters.Sitizens;
using Scripts.Spawner;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.UI.Cards
{
    public class MergeCard : Card
    {
        private readonly int _requiredNumberOfSitizens = 2;

        private ISpawnerService _spawnerService;
        private SitizenSpawner _sitizenSpawner;

        public event Action OnClicked;
        public override event Action<int> CardBought;

        private protected override void OnEnable()
        {
            base.OnEnable();

            _spawnerService = AllServices.Container.Single<ISpawnerService>();
            _sitizenSpawner = _spawnerService.CurrentSitizenSpawner;
            _priceText.text = _saveLoadService.CardsPricesProgress.MergeCardPrice.ToString();

            _sitizenSpawner.NumberOfSitizensChanged += ChangeColor;
        }

        private protected override void OnDisable()
        {
            base.OnDisable();

            _sitizenSpawner.NumberOfSitizensChanged -= ChangeColor;
        }

        private protected override void OnButtonClicked()
        {
            if (_playerMoneyService.Money >= _saveLoadService.CardsPricesProgress.MergeCardPrice && _sitizenSpawner.Sitizens.Count >= _requiredNumberOfSitizens)
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
            List<Sitizen> firstLevelSitizens = _sitizenSpawner.Sitizens.FindAll(p => p.GetComponent<Sitizen>().TypeId == SitizenTypeId.FirstSitizen);

            if (_playerMoneyService.Money >= _saveLoadService.CardsPricesProgress.MergeCardPrice && firstLevelSitizens.Count >= _requiredNumberOfSitizens)
                _image.DOColor(Color.green, _timeOfChangingColor);

            else
                base.ChangeColor();
        }
    }
}
