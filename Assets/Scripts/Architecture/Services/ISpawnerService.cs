using Scripts.Spawner;

namespace Scripts.Architecture.Services
{
    public interface ISpawnerService : IService
    {
        SitizenSpawner CurrentSitizenSpawner { get; }
        ZombieSpawner CurrentZombieSpawner { get; }
    }
}
