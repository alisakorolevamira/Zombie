using Scripts.Architecture.Factory;
using Scripts.Architecture.Services;
using Scripts.Characters;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Spawner
{
    public class ZombieSpawner : MonoBehaviour
    {
        private const string FirstLevelZombiePath = "Prefabs/Zombies/Zombie1";
        private const string SecondLevelZombiePath = "Prefabs/Zombies/Zombie2";
        private const string ThirdLevelZombiePath = "Prefabs/Zombies/Zombie3";
        private const string FourthLevelZombiePath = "Prefabs/Zombies/Zombie4";
        private const string FifthLevelZombiePath = "Prefabs/Zombies/Zombie5";
        private const int FirstLevelIndex = 2;
        private const int SecondLevelIndex = 3;
        private const int ThirdLevelIndex = 4;
        private const int FourthLevelIndex = 5;
        private const int FifthLevelIndex = 6;
        private IGameFactory _factory;
        private string _path;

        public void CreateZombie()
        {
            _factory = AllServices.Container.Single<IGameFactory>();
            GetPath();
            _factory.SpawnZombie(_path).GetComponent<Zombie>();
        }

        private void GetPath()
        {
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;

            switch (sceneIndex)
            {
                case FirstLevelIndex:
                    _path = FirstLevelZombiePath;
                    break;
                case SecondLevelIndex:
                    _path = SecondLevelZombiePath;
                    break;
                case ThirdLevelIndex:
                    _path = ThirdLevelZombiePath;
                    break;
                case FourthLevelIndex:
                    _path = FourthLevelZombiePath;
                    break;
                case FifthLevelIndex:
                    _path = FifthLevelZombiePath;
                    break;
                default:
                    break;
            }
        }
    }
}
