using Scripts.Spawner;

namespace Scripts.Architecture.Services
{
    public interface ISpawnerService : IService
    {
        PanelSpawner CurrentPanelSpawner { get; }
        SitizenSpawner CurrentSitizenSpawner { get; }
        ZombieSpawner CurrentZombieSpawner { get; }
    }
}
