using Architecture.ServicesInterfaces;
using Characters.GameLevel.Citizens;
using UnityEngine;

namespace Architecture.Factory
{
    public interface IGameFactory : IService
    {
        GameObject SpawnCitizen(SpawnPoint spawnPoint, string path);
        GameObject SpawnObject(string path);
        GameObject SpawnZombie(GameObject prefab);
    }
}