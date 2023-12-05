using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;
using Scripts.Architecture.Services;

namespace Scripts.UI.Cards
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(Image))]

    public abstract class Card : Panel
    {
        [SerializeField] private protected TMP_Text _priceText;
        [SerializeField] private protected int _price;

        private protected const int _timeOfChangingColor = 1;
        private protected const int _coefficientOfInceasing = 2;

        private protected IPlayerMoneyService _playerMoneyService;
        private protected Image _image;
        private protected Button _button;
        private protected int _currentPrice;

        public abstract event UnityAction<int> CardBought;

        private protected virtual void OnEnable()
        {
            _image = GetComponent<Image>();
            _button = GetComponent<Button>();

            _playerMoneyService = AllServices.Container.Single<IPlayerMoneyService>();

            _playerMoneyService.MoneyChanged += ChangeColor;
            _button.onClick.AddListener(OnButtonClicked);

            _currentPrice = _price;
            _priceText.text = _currentPrice.ToString();
        }

        private protected virtual void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClicked);
            _playerMoneyService.MoneyChanged -= ChangeColor;
        }

        private protected virtual void Start()
        {
            Open();
        }

        private protected virtual void OnButtonClicked()
        {
            _currentPrice *= _coefficientOfInceasing;
            _priceText.text = _currentPrice.ToString();
        }

        private protected virtual void ChangeColor()
        {
            if (_playerMoneyService.Money() >= _currentPrice)
            {
                _image.DOColor(Color.green, _timeOfChangingColor);
            }

            else
            {
                _image.DOColor(Color.white, _timeOfChangingColor);
            }
        }
    }
}
