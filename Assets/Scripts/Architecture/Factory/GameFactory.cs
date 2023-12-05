using Scripts.Characters.Sitizens;
using UnityEngine;

namespace Scripts.Architecture.Factory
{
    public class GameFactory : IGameFactory
    {
        public GameObject SpawnSitizen(SpawnPoint spawnPoint, string path)
        {
            GameObject sitizenPrefab = Resources.Load<GameObject>(path);

            return Object.Instantiate(sitizenPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
        }

        public GameObject SpawnZombie(string path)
        {
            GameObject zombiePrefab = Resources.Load<GameObject>(path);

            return Object.Instantiate(zombiePrefab, zombiePrefab.transform.position, zombiePrefab.transform.rotation);
        }
    }
}
