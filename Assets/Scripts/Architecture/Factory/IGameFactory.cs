using Scripts.Architecture.Services;
using Scripts.Characters.Citizens;
using UnityEngine;

namespace Scripts.Architecture.Factory
{
    public interface IGameFactory : IService
    {
        GameObject SpawnCitizen(SpawnPoint spawnPoint, string path);
        GameObject SpawnObject(string path);
        GameObject SpawnZombie(GameObject prefab);
    }
}