using System;
using System.Collections.Generic;
using System.Linq;
using Architecture.GameLevel;
using Architecture.ServicesInterfaces.GameLevel;
using Constants;
using UnityEngine;

namespace Architecture.Services.GameLevel
{
    public class LevelService : ILevelService
    {
        private List<LevelSO> _levelsSO;

        public event Action<Architecture.GameLevel.Level> LevelAvailable;
        public event Action<Architecture.GameLevel.Level> LevelComplited;

        public List<Architecture.GameLevel.Level> Levels { get; private set; } = new();

        public void Initialize()
        {
            _levelsSO = Resources.LoadAll<LevelSO>(LevelConstants.LevelsPath).ToList();

            foreach (LevelSO levelSO in _levelsSO)
            {
                Architecture.GameLevel.Level level = new(levelSO.MeduimScore, levelSO.HighScore, levelSO.Name,
                    levelSO.Id, levelSO.IsAvailable, levelSO.Zombie, this);

                Levels.Add(level);
            }
        }

        public Architecture.GameLevel.Level FindLevelByName(string levelName)
        {
            Architecture.GameLevel.Level level = Levels.Find(x => x.Name == levelName);

            return level;
        }

        public string FindNextLevel(string activeLevelName)
        {
            Architecture.GameLevel.Level activeLevel = FindLevelByName(activeLevelName);

            if (activeLevel != null && activeLevel.Id != LevelConstants.MaximumNumberOfLevels)
            {
                Architecture.GameLevel.Level nextLevel = FindLevelById(activeLevel.Id + LevelConstants.IndexCoefficient);
                return nextLevel.Name;
            }

            else
                return LevelConstants.Menu;
        }

        public void LevelComplete(Architecture.GameLevel.Level level)
        {
            string nextLevelName = FindNextLevel(level.Name);
            Architecture.GameLevel.Level nextLevel = FindLevelByName(nextLevelName);

            LevelComplited?.Invoke(level);

            if(nextLevel != null)
                LevelAvailable?.Invoke(nextLevel);
        }

        private Architecture.GameLevel.Level FindLevelById(int id)
        {
            Architecture.GameLevel.Level level = Levels.Find(x => x.Id == id);

            return level;
        }
    }
}