using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.Architecture.Services
{
    public class LevelService : ILevelService
    {
        private readonly int _indexCoefficient = 1;

        private List<LevelSO> _levelsSO;

        public event Action<Level> LevelAvailable;
        public event Action<Level> LevelComplited;

        public List<Level> Levels { get; private set; } = new();

        public void Initialize()
        {
            _levelsSO = Resources.LoadAll<LevelSO>(Constants.ScenesPath).ToList();

            foreach (LevelSO levelSO in _levelsSO)
            {
                Level level = new(levelSO.MeduimScore, levelSO.HighScore, levelSO.Name,
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
            Level lastLevel = Levels.Find(x => x.Id == Constants.MaximumNumberOfScenes);

            if (activeLevel != null && activeLevel != lastLevel)
            {
                Level nextLevel = FindLevelById(activeLevel.Id + _indexCoefficient);
                LevelAvailable?.Invoke(nextLevel);

                return nextLevel.Name;
            }

            else
                return Constants.Menu;
        }

        public void LevelComplite(Level scene) => LevelComplited?.Invoke(scene);

        private Level FindLevelById(int id)
        {
            Level level = Levels.Find(x => x.Id == id);

            return level;
        }
    }
}
