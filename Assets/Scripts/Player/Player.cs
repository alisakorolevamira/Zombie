using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private Card [] _cards;

    private void OnEnable()
    {
        Game.Run();

        foreach (var card in _cards)
        {
            card.CardBought += OnCardBought;
        }
    }

    private void OnDisable()
    {
        foreach (var card in _cards)
        {
            card.CardBought -= OnCardBought;
        }
    }

    private void OnCardBought(int price)
    {
        var playersMoneyInteractor = Game.GetInteractor<PlayersMoneyInteractor>();
        playersMoneyInteractor.SpendMoney(price);
    }
}
