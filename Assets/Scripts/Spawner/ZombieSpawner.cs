using Scripts.Architecture.Factory;
using Scripts.Architecture.Services;
using Scripts.Characters;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Spawner
{
    public class ZombieSpawner : MonoBehaviour
    {
        private readonly int _sceneIndexCoefficient = 2;

        private IGameFactory _factory;
        private string _path;

        public void CreateZombie()
        {
            _factory = AllServices.Container.Single<IGameFactory>();

            GetPath();
            _factory.SpawnZombie(_path);
        }

        private void GetPath()
        {
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;

            _path = $"{Constants.ZombiePath}{sceneIndex - _sceneIndexCoefficient}";
        }
    }
}
