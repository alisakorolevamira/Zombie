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

        public GameObject SpawnObject(string path)
        {
            GameObject objectPrefab = Resources.Load<GameObject>(path);

            return Object.Instantiate(objectPrefab, objectPrefab.transform.position, objectPrefab.transform.rotation);
        }
    }
}
