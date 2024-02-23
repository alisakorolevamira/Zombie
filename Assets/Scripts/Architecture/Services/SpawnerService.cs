using Scripts.Spawner;

namespace Scripts.Architecture.Services
{
    public class SpawnerService : ISpawnerService
    {
        public SpawnerService(SitizenSpawner sitizenSpawner, ZombieSpawner zombieSpawner)
        {
            SitizenSpawner = sitizenSpawner;
            ZombieSpawner = zombieSpawner;
        }

        public SitizenSpawner SitizenSpawner { get; }
        public ZombieSpawner ZombieSpawner { get; }
    }
}
