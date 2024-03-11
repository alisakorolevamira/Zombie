using Scripts.Spawner;

namespace Scripts.Architecture.Services
{
    public interface ISpawnerService : IService
    {
        SitizenSpawner SitizenSpawner { get; }
        ZombieSpawner ZombieSpawner { get; }

        void Initialize();
    }
}
