using Scripts.Architecture.Factory;
using Scripts.Architecture.Services;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Spawner
{
    public class ZombieSpawner : MonoBehaviour
    {
        private readonly int _sceneIndexCoefficient = 1;

        private IGameFactory _factory;
        private string _path;

        private void Start()
        {
            _factory = AllServices.Container.Single<IGameFactory>();
        }

        public void CreateZombie()
        {
            GetPath();
            _factory.SpawnObject(_path);
        }

        private void GetPath()
        {
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;

            _path = $"{Constants.ZombiePath}{sceneIndex - _sceneIndexCoefficient}";
        }
    }
}
