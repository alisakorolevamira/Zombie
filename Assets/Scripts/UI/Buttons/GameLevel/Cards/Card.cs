using System;
using Architecture.Services;
using Architecture.ServicesInterfaces.Data;
using Architecture.ServicesInterfaces.Player;
using Audio;
using Constants.UI;
using DG.Tweening;
using TMPro;
using UI.Panels.Common;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Buttons.GameLevel.Cards
{
    [RequireComponent(typeof(Button))]

    public abstract class Card : Panel
    {
        [SerializeField] private protected TMP_Text _priceText;
        [SerializeField] private protected Button _button;
        [SerializeField] private ShortEffectAudio _shortEffectAudio;

        private protected IPlayerMoneyService _playerMoneyService;
        private protected ICardsPricesDataService _cardsPricesDataService;

        public abstract event Action<int> CardBought;

        public int Price { get; protected set; }

        public override void Open()
        {
            _playerMoneyService.MoneyChanged += ChangeColor;
            _button.onClick.AddListener(OnButtonClicked);
        }

        public override void Close()
        {
            _playerMoneyService = AllServices.Container.Single<IPlayerMoneyService>();
            _cardsPricesDataService = AllServices.Container.Single<ICardsPricesDataService>();

            _button.onClick.RemoveListener(OnButtonClicked);
            _playerMoneyService.MoneyChanged -= ChangeColor;
        }

        private protected virtual void OnButtonClicked() => _shortEffectAudio.PlayOneShot();

        private protected virtual void ChangeColor()
        {
            _image.DOColor(Color.white, CardsConstants.TimeOfChangingColor);
        }
    }
}