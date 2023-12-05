using Scripts.Architecture.Services;
using Scripts.Spawner;
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
        }

        private protected override void OnButtonClicked()
        {

            if (_playerMoneyService.Money() >= _currentPrice && _sitizenSpawner.Sitizens != null)
            {
                CardBought?.Invoke(_currentPrice);

                foreach (var sitizen in _sitizenSpawner.Sitizens)
                {
                    sitizen.AddSpeed();
                }

                base.OnButtonClicked();
            }
        }
    }
}
