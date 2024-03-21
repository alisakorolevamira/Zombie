using Scripts.Spawner;

namespace Scripts.Architecture.Services
{
    public interface ISpawnerService : IService
    {
        CitizenSpawner CitizenSpawner { get; }
        ZombieSpawner ZombieSpawner { get; }

        void Initialize();
    }
}
