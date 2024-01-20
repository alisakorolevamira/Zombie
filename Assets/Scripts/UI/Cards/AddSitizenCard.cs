using DG.Tweening;
using Scripts.Architecture.Services;
using Scripts.Spawner;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts.UI.Cards
{
    public class AddSitizenCard : Card
    {
        private ISpawnerService _spawnerService;
        private SitizenSpawner _sitizenSpawner;

        public event UnityAction OnClicked;
        public override event UnityAction<int> CardBought;

        private protected override void OnEnable()
        {
            base.OnEnable();

            _spawnerService = AllServices.Container.Single<ISpawnerService>();
            _sitizenSpawner = _spawnerService.CurrentSitizenSpawner;
            _priceText.text = _progressService.Progress.AddSitizenCardPrice.ToString();

            _sitizenSpawner.NumberOfSitizensChanged += ChangeColor;
        }

        private protected override void OnDisable()
        {
            base.OnDisable();

            _sitizenSpawner.NumberOfSitizensChanged -= ChangeColor;
        }

        private protected override void OnButtonClicked()
        {
            if (_playerMoneyService.Money() >= _progressService.Progress.AddSitizenCardPrice && _sitizenSpawner.CheckAmountOfSitizens())
            {
                OnClicked?.Invoke();
                CardBought?.Invoke(_progressService.Progress.AddSitizenCardPrice);

                _progressService.Progress.AddSitizenCardPrice *= _coefficientOfInceasing;
                _priceText.text = _progressService.Progress.AddSitizenCardPrice.ToString();

                base.OnButtonClicked();
            }
        }

        private protected override void ChangeColor()
        {

            if (_playerMoneyService.Money() >= _progressService.Progress.AddSitizenCardPrice && _sitizenSpawner.CheckAmountOfSitizens())
            {
                _image.DOColor(Color.green, _timeOfChangingColor);
            }

            else
            {
                base.ChangeColor();
            }
        }
    }
}
