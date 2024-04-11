using Spawner;

namespace Architecture.ServicesInterfaces
{
    public interface ISpawnerService : IService
    {
        CitizenSpawner CitizenSpawner { get; }
        ZombieSpawner ZombieSpawner { get; }

        void Initialize();
    }
}