using Scripts.Progress;
using Scripts.UI.Cards;
using UnityEngine;

namespace Scripts.Architecture.Services
{
    public class CardsPricesDataService : ICardsPricesDataService
    {
        private readonly string _key = "CardsProgress";
        private readonly IUIPanelService _panelService;
        private readonly int _addCitizenCardMinimalPrice = 10;
        private readonly int _mergeCardMinimalPrice = 50;
        private readonly int _addSpeedCardMinimalPrice = 40;
        private readonly int _doubleRewardCardMinimalPrice = 100;

        private string _file;

        public CardsPricesDataService(IUIPanelService panelService)
        {
            _panelService = panelService;
        }

        public CardsPricesProgress CardsPricesProgress { get; private set; } = new();

        public void LoadData()
        {
            _file = PlayerPrefs.GetString(_key, string.Empty);

            if (_file == string.Empty)
                SetNewData();

            else
                CardsPricesProgress = JsonUtility.FromJson<CardsPricesProgress>(_file);
        }

        public void ResetData()
        {
            PlayerPrefs.DeleteKey(_key);

            SetNewData();
        }

        public void SaveData()
        {
            _file = JsonUtility.ToJson(CardsPricesProgress);
            PlayerPrefs.SetString(_key, _file);
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
            CardsPricesProgress.AddCitizenCardPrice = _addCitizenCardMinimalPrice;
            CardsPricesProgress.MergeCardPrice = _mergeCardMinimalPrice;
            CardsPricesProgress.AddSpeedCardPrice = _addSpeedCardMinimalPrice;
            CardsPricesProgress.DoubleRewardCardPrice = _doubleRewardCardMinimalPrice;

            SaveData();
        }
    }
}