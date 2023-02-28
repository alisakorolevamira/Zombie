using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Image))]

public abstract class Card : MonoBehaviour
{
    [SerializeField] private protected Player _player;
    [SerializeField] private protected TMP_Text _priceText;
    [SerializeField] private protected int _price;

    private protected Color _currentColor;
    private protected Image _image;
    private protected Button _button;

    private protected int _timeOfChangingColor = 1;

    public abstract event UnityAction<int> CardBought;

    private protected virtual void OnEnable()
    {
        _image = GetComponent<Image>();
        _button = GetComponent<Button>();

        _button.onClick.AddListener(OnButtonClicked);
        _player.MoneyChanged += ChangeColor;
    }

    private protected virtual void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClicked);
        _player.MoneyChanged -= ChangeColor;
    }

    private protected virtual void Start()
    {
        _priceText.text = _price.ToString();
        _currentColor = _image.color;
    }

    private protected virtual void OnButtonClicked() { }

    private protected virtual void ChangeColor()
    {
        if (_player.Money >= _price)
        {
            _image.DOColor(_currentColor, _timeOfChangingColor);
        }

        else
        {
            _image.DOColor(Color.white, _timeOfChangingColor);
        }
    }
}
