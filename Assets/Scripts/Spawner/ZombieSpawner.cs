using Scripts.Architecture.Factory;
using Scripts.Architecture.Services;
using UnityEngine;

namespace Scripts.Spawner
{
    public class ZombieSpawner : MonoBehaviour
    {
        private IGameFactory _factory;
        private ILevelService _sceneService;

        private void Start()
        {
            _factory = AllServices.Container.Single<IGameFactory>();
            _sceneService = AllServices.Container.Single<ILevelService>();
        }

        public void CreateZombie(string sceneName)
        {
            var scene = _sceneService.FindLevelByName(sceneName);
            _factory.SpawnZombie(scene.Zombie);
        }
    }
}
