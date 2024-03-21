using Scripts.Architecture.Factory;
using Scripts.Architecture.Services;
using UnityEngine;

namespace Scripts.Spawner
{
    public class ZombieSpawner : MonoBehaviour
    {
        private IGameFactory _factory;
        private ISceneService _sceneService;

        private void Start()
        {
            _factory = AllServices.Container.Single<IGameFactory>();
            _sceneService = AllServices.Container.Single<ISceneService>();
        }

        public void CreateZombie(string sceneName)
        {
            var scene = _sceneService.FindSceneByName(sceneName);
            _factory.SpawnZombie(scene.Zombie);
        }
    }
}
