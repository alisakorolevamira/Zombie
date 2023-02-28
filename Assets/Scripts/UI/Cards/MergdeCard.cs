using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MergdeCard : Card
{
    [SerializeField] private SitizensSpawner _spawner;
    private int _requiredNumberOfSitizens = 2;

    public event UnityAction OnClicked;
    public override event UnityAction<int> CardBought;

    private protected override void OnEnable()
    {
        base.OnEnable();

        _spawner.NumberOfSitizensChanged += ChangeColor;
    }

    private protected override void OnDisable()
    {
        base.OnDisable();

        _spawner.NumberOfSitizensChanged -= ChangeColor;
    }

    private protected override void OnButtonClicked()
    {
        if (_player.Money >= _price && _spawner.NumberOfSitizens >= _requiredNumberOfSitizens)
        {
            OnClicked?.Invoke();
            CardBought?.Invoke(-_price);
            _price = _price * 2;
            _priceText.text = _price.ToString();
        }
    }

    private protected override void ChangeColor()
    {
        if (_player.Money >= _price && _spawner.NumberOfSitizens >= _requiredNumberOfSitizens)
        {
            _image.DOColor(_currentColor, _timeOfChangingColor);
        }

        else
        {
            _image.DOColor(Color.white, _timeOfChangingColor);
        }
    }
}
