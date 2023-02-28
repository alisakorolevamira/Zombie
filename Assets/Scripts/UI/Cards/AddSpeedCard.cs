using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class AddSpeedCard : Card
{
    [SerializeField] private SitizensSpawner _spawner;

    public override event UnityAction<int> CardBought;

    private protected override void OnButtonClicked()
    {
        var sitizens = _spawner.GetSitizens();

        if (_player.Money >= _price && sitizens != null)
        {
            CardBought?.Invoke(-_price);
            _price = _price * 2;
            _priceText.text = _price.ToString();

            foreach (var sitizen in sitizens)
            {
                sitizen.AddSpeed();
            }
        }
    }
}
