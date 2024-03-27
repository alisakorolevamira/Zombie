using UnityEngine;

namespace Scripts.Architecture.Services
{
    public class SaveLoadService : ISaveLoadService
    {
        private readonly IPlayerDataService _playerDataService;
        private readonly IZombieDataService _zombieDataService;
        private readonly ILevelDataService _levelDataService;
        private readonly ICardsPricesDataService _cardsPricesDataService;

        public SaveLoadService(IPlayerDataService playerDataService, IZombieDataService zombieDataService, ILevelDataService levelDataService,
            ICardsPricesDataService cardsPricesDataService)
        {
            _playerDataService = playerDataService;
            _zombieDataService = zombieDataService;
            _levelDataService = levelDataService;
            _cardsPricesDataService = cardsPricesDataService;
        }

        public void LoadProgress()
        {
            _playerDataService.LoadData();
            _zombieDataService.LoadData();
            _cardsPricesDataService.LoadData();
            _levelDataService.LoadData();
        }

        public void SaveProgress()
        {
            UpdateData();

            _playerDataService.SaveData();
            _zombieDataService.SaveData();
            _cardsPricesDataService.SaveData();
            _levelDataService.SaveData();

            PlayerPrefs.Save();
        }

        public void ResetProgress()
        {
            _playerDataService.ResetData(Constants.Menu);
            _zombieDataService.ResetData();
            _cardsPricesDataService.ResetData();
            _levelDataService.ResetData();

            PlayerPrefs.Save();
        }

        private void UpdateData()
        {
            _playerDataService.UpdateData();
            _zombieDataService.UpdateData();
            _cardsPricesDataService.UpdateData();
            _levelDataService.UpdateData();
        }
    }
}