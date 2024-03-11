using Scripts.Architecture.Factory;
using Scripts.Spawner;

namespace Scripts.Architecture.Services
{
    public class SpawnerService : ISpawnerService
    {
        private readonly IGameFactory _gameFactory;

        public SpawnerService(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        public SitizenSpawner SitizenSpawner { get; private set; }
        public ZombieSpawner ZombieSpawner { get; private set; }

        public void Initialize()
        {
            var spawner = _gameFactory.SpawnObject(Constants.SpawnersPath);

            SitizenSpawner = spawner.GetComponent<SitizenSpawner>();
            ZombieSpawner = spawner.GetComponent<ZombieSpawner>();
        }
    }
}
