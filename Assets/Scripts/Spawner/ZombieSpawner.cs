using Architecture.Factory;
using Architecture.GameLevel;
using Architecture.Services;
using Architecture.ServicesInterfaces.GameLevel;
using UnityEngine;

namespace Spawner
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

        public void CreateZombie(string levelName)
        {
            Level level = _sceneService.FindLevelByName(levelName);
            _factory.SpawnZombie(level.Zombie);
        }
    }
}