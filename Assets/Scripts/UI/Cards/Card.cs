using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Scripts.Architecture.Services;
using DG.Tweening;
using Scripts.UI.Panels;
using System;

namespace Scripts.UI.Cards
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(Image))]

    public abstract class Card : Panel
    {
        private protected const int _timeOfChangingColor = 1;
        private protected const int _coefficientOfInceasing = 2;

        [SerializeField] private protected TMP_Text _priceText;

        private protected IPlayerMoneyService _playerMoneyService;
        private protected ISaveLoadService _saveLoadService;
        private protected Button _button;
        private protected AudioSource _audioSource;

        public abstract event Action<int> CardBought;

        private protected virtual void OnEnable()
        {
            _button = GetComponent<Button>();
            _audioSource = GetComponentInParent<AudioSource>();
            _image = GetComponent<Image>();

            _playerMoneyService = AllServices.Container.Single<IPlayerMoneyService>();
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();

            _playerMoneyService.MoneyChanged += ChangeColor;
            _button.onClick.AddListener(OnButtonClicked);
        }

        private protected virtual void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClicked);
            _playerMoneyService.MoneyChanged -= ChangeColor;
        }

        private protected virtual void OnButtonClicked()
        {
            _audioSource.PlayOneShot(_audioSource.clip);
            _saveLoadService.SaveProgress();
        }

        private protected virtual void ChangeColor()
        {
            _image.DOColor(Color.white, _timeOfChangingColor);
        }
    }
}
