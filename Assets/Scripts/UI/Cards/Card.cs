using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Scripts.Architecture.Services;
using DG.Tweening;

namespace Scripts.UI.Cards
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(Image))]

    public abstract class Card : Panel
    {
        [SerializeField] private protected TMP_Text _priceText;

        private protected const int _timeOfChangingColor = 1;
        private protected const int _coefficientOfInceasing = 2;

        private protected IPlayerMoneyService _playerMoneyService;
        private protected ICardsPricesProgressService _progressService;
        private protected Image _image;
        private protected Button _button;
        private protected AudioSource _audioSource;

        public abstract event UnityAction<int> CardBought;

        private protected virtual void OnEnable()
        {
            _image = GetComponent<Image>();
            _button = GetComponent<Button>();
            _audioSource = GetComponentInParent<AudioSource>();

            _playerMoneyService = AllServices.Container.Single<IPlayerMoneyService>();
            _progressService = AllServices.Container.Single<ICardsPricesProgressService>();

            _playerMoneyService.MoneyChanged += ChangeColor;
            _button.onClick.AddListener(OnButtonClicked);
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
            _audioSource.PlayOneShot(_audioSource.clip);
            _progressService.SaveProgress();
        }

        private protected virtual void ChangeColor()
        {
            _image.DOColor(Color.white, _timeOfChangingColor);
        }
    }
}
