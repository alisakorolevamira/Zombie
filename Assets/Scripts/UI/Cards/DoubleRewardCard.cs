using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoubleRewardCard : Card
{
    [SerializeField] private Zombie _zombie;

    public override event UnityAction<int> CardBought;

    private protected override void OnButtonClicked()
    {
        if (_player.Money >= _price)
        {
            CardBought?.Invoke(-_price);
            _price = _price * 2;
            _priceText.text = _price.ToString();

            _zombie.DoubleReward();
        }
    }
}
