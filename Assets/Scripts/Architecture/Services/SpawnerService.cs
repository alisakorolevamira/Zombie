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

        public CitizenSpawner CitizenSpawner { get; private set; }
        public ZombieSpawner ZombieSpawner { get; private set; }

        public void Initialize()
        {
            var spawner = _gameFactory.SpawnObject(Constants.SpawnersPath);

            CitizenSpawner = spawner.GetComponent<CitizenSpawner>();
            ZombieSpawner = spawner.GetComponent<ZombieSpawner>();
        }
    }
}
