using System;

namespace Scripts.Architecture.Services
{
    public interface ISceneService : IService
    {
        event Action<SceneSO> SceneAvailable;
        event Action<SceneSO> SceneComplited;

        SceneSO FindSceneByName(string sceneName);
        string FindNextScene(string activeScene);

        void Initialize();
        void LevelComplited(SceneSO scene);
    }
}