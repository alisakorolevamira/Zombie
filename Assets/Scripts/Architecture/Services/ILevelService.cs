using System;
using System.Collections.Generic;

namespace Scripts.Architecture.Services
{
    public interface ILevelService : IService
    {
        event Action<Level> LevelAvailable;
        event Action<Level> LevelComplited;

        List<Level> Levels { get; }

        Level FindLevelByName(string sceneName);
        string FindNextLevel(string activeScene);

        void Initialize();
        void LevelComplete(Level level);
    }
}