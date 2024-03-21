using Scripts.Architecture.Services;
using Scripts.UI.Cards;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Characters
{
    public class Player : MonoBehaviour
    {
        private List<Card> _cards;
        private IPlayerMoneyService _moneyService;
        private IUIPanelService _panelService;

        private void OnDisable()
        {
            foreach (Card card in _cards)
            {
                card.CardBought -= OnCardBought;
            }
        }

        private void Start()
        {
            _panelService = AllServices.Container.Single<IUIPanelService>();
            _moneyService = AllServices.Container.Single<IPlayerMoneyService>();

            _cards = new List<Card>
            {
                _panelService.GetCard<AddCitizenCard>(),
                _panelService.GetCard<AddSpeedCard>(),
                _panelService.GetCard<MergeCard>(),
                _panelService.GetCard<DoubleRewardCard>()
            };

            foreach (Card card in _cards)
            {
                card.CardBought += OnCardBought;
            }
        }

        private void OnCardBought(int price)
        {
            _moneyService.SpendMoney(price);
        }
    }
}