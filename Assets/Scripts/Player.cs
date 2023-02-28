using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private Zombie _zombie;
    [SerializeField] private Card [] _cards;
    private int _money;

    public event UnityAction MoneyChanged;
    public int Money { get { return _money; } }

    private void OnEnable()
    {
        _zombie.RewardGived += OnChangeMoney;

        foreach (var card in _cards)
        {
            card.CardBought += OnChangeMoney;
        }
    }

    private void OnDisable()
    {
        _zombie.RewardGived -= OnChangeMoney;

        foreach (var card in _cards)
        {
            card.CardBought -= OnChangeMoney;
        }
    }

    private void OnChangeMoney(int amount)
    {
        _money += amount;
        MoneyChanged?.Invoke();
    }
}
