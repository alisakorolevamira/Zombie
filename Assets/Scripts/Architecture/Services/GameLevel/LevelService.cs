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

        public event Action<Level> LevelAvailable;
        public event Action<Level> LevelCompleted;

        public List<Level> Levels { get; } = new ();

        public void Initialize()
        {
            _levelsSO = Resources.LoadAll<LevelSO>(LevelConstants.LevelsPath).ToList();

            foreach (LevelSO levelSO in _levelsSO)
            {
                Level level = new (levelSO.MediumScore, levelSO.HighScore, levelSO.Name,
                    levelSO.Id, levelSO.IsAvailable, levelSO.Zombie, this);

                Levels.Add(level);
            }
        }

        public Level FindLevelByName(string levelName)
        {
            Level level = Levels.Find(x => x.Name == levelName);

            return level;
        }

        public string FindNextLevel(string activeLevelName)
        {
            Level activeLevel = FindLevelByName(activeLevelName);

            if (activeLevel != null && activeLevel.Id != LevelConstants.MaximumNumberOfLevels)
            {
                Level nextLevel = FindLevelById(activeLevel.Id + LevelConstants.IndexCoefficient);
                return nextLevel.Name;
            }

            return LevelConstants.Menu;
        }

        public void LevelComplete(Level level)
        {
            string nextLevelName = FindNextLevel(level.Name);
            Level nextLevel = FindLevelByName(nextLevelName);

            LevelCompleted?.Invoke(level);

            if (nextLevel != null)
                LevelAvailable?.Invoke(nextLevel);
        }

        private Level FindLevelById(int id)
        {
            Level level = Levels.Find(x => x.Id == id);

            return level;
        }
    }
}