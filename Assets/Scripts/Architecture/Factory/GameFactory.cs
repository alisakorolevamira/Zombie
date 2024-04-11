using Characters.GameLevel.Citizens;
using UnityEngine;

namespace Architecture.Factory
{
    public class GameFactory : IGameFactory
    {
        public GameObject SpawnCitizen(SpawnPoint spawnPoint, string path)
        {
            GameObject citizenPrefab = Resources.Load<GameObject>(path);

            return Object.Instantiate(citizenPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
        }

        public GameObject SpawnObject(string path)
        {
            GameObject objectPrefab = Resources.Load<GameObject>(path);

            return Object.Instantiate(objectPrefab, objectPrefab.transform.position, objectPrefab.transform.rotation);
        }

        public GameObject SpawnZombie(GameObject prefab)
        {
            return Object.Instantiate(prefab, prefab.transform.position, prefab.transform.rotation);
        }
    }
}