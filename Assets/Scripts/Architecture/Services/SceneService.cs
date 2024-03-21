using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.Architecture.Services
{
    public class SceneService : ISceneService
    {
        private readonly int _indexCoefficient = 1;

        private List<SceneSO> _scenes;

        public event Action<SceneSO> SceneAvailable;
        public event Action<SceneSO> SceneComplited;

        public void Initialize()
        {
            _scenes = Resources.LoadAll<SceneSO>(Constants.ScenesPath).ToList();

            foreach (SceneSO scene in _scenes)
            {
                scene.AddListener(this);
            }
        }

        public SceneSO FindSceneByName(string sceneName)
        {
            SceneSO scene = _scenes.Find(x => x.Name == sceneName);

            return scene;
        }

        public string FindNextScene(string activeSceneName)
        {
            SceneSO activeScene = FindSceneByName(activeSceneName);
            SceneSO lastScene = _scenes.Find(x => x.Id == Constants.MaximumNumberOfScenes);

            if (activeScene != null && activeScene != lastScene)
            {
                SceneSO nextScene = FindSceneById(activeScene.Id + _indexCoefficient);
                SceneAvailable?.Invoke(nextScene);

                return nextScene.Name;
            }

            else
                return Constants.Menu;
        }

        public void LevelComplited(SceneSO scene)
        {
            SceneComplited?.Invoke(scene);
        }

        private SceneSO FindSceneById(int id)
        {
            SceneSO scene = _scenes.Find(x => x.Id == id);

            return scene;
        }
    }
}
