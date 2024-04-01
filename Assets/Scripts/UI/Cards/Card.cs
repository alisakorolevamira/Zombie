using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Scripts.Architecture.Services;
using DG.Tweening;
using Scripts.UI.Panels;
using System;
using Scripts.Audio;
using Scripts.Constants;

namespace Scripts.UI.Cards
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