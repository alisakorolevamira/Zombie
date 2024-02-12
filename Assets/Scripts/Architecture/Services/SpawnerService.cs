using Scripts.Spawner;

namespace Scripts.Architecture.Services
{
    public class SpawnerService : ISpawnerService
    {
        public SpawnerService(SitizenSpawner sitizenSpawner, ZombieSpawner zombieSpawner)
        {
            CurrentSitizenSpawner = sitizenSpawner;
            CurrentZombieSpawner = zombieSpawner;
        }

        public SitizenSpawner CurrentSitizenSpawner { get; }
        public ZombieSpawner CurrentZombieSpawner { get; }
    }
}
