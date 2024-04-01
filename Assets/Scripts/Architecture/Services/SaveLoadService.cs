using Scripts.Constants;
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
            UpdateProgress();

            _playerDataService.SaveData();
            _zombieDataService.SaveData();
            _cardsPricesDataService.SaveData();
            _levelDataService.SaveData();

            PlayerPrefs.Save();
        }

        public void ResetAllProgress()
        {
            _playerDataService.ResetData(LevelConstants.Menu);
            _zombieDataService.ResetData();
            _cardsPricesDataService.ResetData();
            _levelDataService.ResetData();

            PlayerPrefs.Save();
        }

        public void ResetProgressForNextLevel(string sceneName)
        {
            _playerDataService.ResetData(sceneName);
            _zombieDataService.ResetData();
            _cardsPricesDataService.ResetData();
            _levelDataService.UpdateData();

            PlayerPrefs.Save();
        }

        private void UpdateProgress()
        {
            _playerDataService.UpdateData();
            _zombieDataService.UpdateData();
            _cardsPricesDataService.UpdateData();
            _levelDataService.UpdateData();
        }
    }
}