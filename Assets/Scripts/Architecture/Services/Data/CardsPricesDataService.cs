using Architecture.ServicesInterfaces.Data;
using Architecture.ServicesInterfaces.UI;
using Constants.UI;
using Progress;
using UI.Buttons.GameLevel.Cards;
using UnityEngine;

namespace Architecture.Services.Data
{
    public class CardsPricesDataService : ICardsPricesDataService
    {
        private readonly IUIPanelService _panelService;

        private string _file;

        public CardsPricesDataService(IUIPanelService panelService)
        {
            _panelService = panelService;
        }

        public CardsPricesProgress CardsPricesProgress { get; private set; } = new();

        public void LoadData()
        {
            _file = PlayerPrefs.GetString(CardsConstants.Key, string.Empty);

            if (_file == string.Empty)
                SetNewData();

            else
                CardsPricesProgress = JsonUtility.FromJson<CardsPricesProgress>(_file);
        }

        public void ResetData()
        {
            PlayerPrefs.DeleteKey(CardsConstants.Key);
            SetNewData();
        }

        public void SaveData()
        {
            _file = JsonUtility.ToJson(CardsPricesProgress);
            
            PlayerPrefs.SetString(CardsConstants.Key, _file);
        }

        public void UpdateData()
        {
            CardsPricesProgress.AddCitizenCardPrice = _panelService.GetCard<AddCitizenCard>().Price;
            CardsPricesProgress.MergeCardPrice = _panelService.GetCard<MergeCard>().Price;
            CardsPricesProgress.AddSpeedCardPrice = _panelService.GetCard<AddSpeedCard>().Price;
            CardsPricesProgress.DoubleRewardCardPrice = _panelService.GetCard<DoubleRewardCard>().Price;
        }

        private void SetNewData()
        {
            CardsPricesProgress.AddCitizenCardPrice = CardsConstants.AddCitizenCardMinimalPrice;
            CardsPricesProgress.MergeCardPrice = CardsConstants.MergeCardMinimalPrice;
            CardsPricesProgress.AddSpeedCardPrice = CardsConstants.AddSpeedCardMinimalPrice;
            CardsPricesProgress.DoubleRewardCardPrice = CardsConstants.DoubleRewardCardMinimalPrice;

            SaveData();
        }
    }
}