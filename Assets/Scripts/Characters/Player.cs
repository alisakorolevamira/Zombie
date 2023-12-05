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
        private ISpawnerService _spawnerService;

        private void OnEnable()
        {
            _spawnerService = AllServices.Container.Single<ISpawnerService>();
            _cards = new List<Card>
            {
                _spawnerService.CurrentPanelSpawner.GetPanel<AddSitizenCard>(),
                _spawnerService.CurrentPanelSpawner.GetPanel<AddSpeedCard>(),
                _spawnerService.CurrentPanelSpawner.GetPanel<MergeCard>(),
                _spawnerService.CurrentPanelSpawner.GetPanel<DoubleRewardCard>()
            };

            foreach (Card card in _cards)
            {
                card.CardBought += OnCardBought;
            }
        }

        private void OnDisable()
        {
            foreach (Card card in _cards)
            {
                card.CardBought -= OnCardBought;
            }
        }

        private void OnCardBought(int price)
        {
            _moneyService = AllServices.Container.Single<IPlayerMoneyService>();
            _moneyService.SpendMoney(price);
        }
    }
}