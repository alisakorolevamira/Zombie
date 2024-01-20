using DG.Tweening;
using Scripts.Architecture.Services;
using Scripts.Spawner;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts.UI.Cards
{
    public class AddSpeedCard : Card
    {
        private ISpawnerService _spawnerService;
        private SitizenSpawner _sitizenSpawner;

        public override event UnityAction<int> CardBought;

        private protected override void OnEnable()
        {
            base.OnEnable();

            _spawnerService = AllServices.Container.Single<ISpawnerService>();
            _sitizenSpawner = _spawnerService.CurrentSitizenSpawner;
            _priceText.text = _progressService.Progress.AddSpeedCardPrice.ToString();
        }

        private protected override void OnButtonClicked()
        {
            if (_playerMoneyService.Money() >= _progressService.Progress.AddSpeedCardPrice && _sitizenSpawner.Sitizens != null)
            {
                CardBought?.Invoke(_progressService.Progress.AddSpeedCardPrice);

                foreach (var sitizen in _sitizenSpawner.Sitizens)
                {
                    sitizen.AddSpeed();
                }

                _progressService.Progress.AddSpeedCardPrice *= _coefficientOfInceasing;
                _priceText.text = _progressService.Progress.AddSpeedCardPrice.ToString();

                base.OnButtonClicked();
            }
        }

        private protected override void ChangeColor()
        {
            if (_playerMoneyService.Money() >= _progressService.Progress.AddSpeedCardPrice)
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
