using Scripts.Architecture.Services;
using Scripts.Characters.Sitizens;
using UnityEngine;

namespace Scripts.Architecture.Factory
{
    public interface IGameFactory : IService
    {
        GameObject SpawnSitizen(SpawnPoint spawnPoint, string path);
        GameObject SpawnObject(string path);
    }
}