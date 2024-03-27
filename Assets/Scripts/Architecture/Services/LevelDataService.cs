using Scripts.Progress;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Architecture.Services
{
    public class LevelDataService : ILevelDataService
    {
        private readonly ILevelService _levelService;
        private readonly string _key = "LevelProgress";
        private readonly List<LevelProgress> _levelProgresses = new();

        private string _file;

        public LevelDataService(ILevelService levelService)
        {
            _levelService = levelService;
        }

        public void LoadData()
        {
            _levelProgresses.Clear();

            foreach (Level level in _levelService.Levels)
            {
                string key = _key + level.Id;
                _file = PlayerPrefs.GetString(key, string.Empty);

                LevelProgress progress = new();
                progress.Id = level.Id;

                if (_file == string.Empty)
                    SetNewData(progress);

                else
                    progress = JsonUtility.FromJson<LevelProgress>(_file);

                _levelProgresses.Add(progress);
                level.Initialize(progress);
            }

            SaveData();
        }

        public void ResetData()
        {
            foreach (LevelProgress progress in _levelProgresses)
            {
                string key = _key + progress.Id;
                PlayerPrefs.DeleteKey(key);

                SetNewData(progress);
            }

            SaveData();
        }

        public void SaveData()
        {
            foreach (LevelProgress progress in _levelProgresses)
            {
                string key = _key + progress.Id;
                _file = JsonUtility.ToJson(progress);
                PlayerPrefs.SetString(key, _file);
            }
        }

        public void UpdateData()
        {
            foreach (LevelProgress progress in _levelProgresses)
            {
                Level level = _levelService.Levels.Find(x => x.Id == progress.Id);
                progress.Stars = level.AmountOfStars;
                progress.IsAvailable = level.IsAvailable.ToString();
            }

            SaveData();
        }

        private void SetNewData(LevelProgress progress)
        {
            progress.Stars = Constants.DefaultAmountOfStars;
            progress.IsAvailable = Constants.DefaultAvailability;
        }
    }
}