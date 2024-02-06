using Scripts.Spawner;

namespace Scripts.Architecture.Services
{
    public class SpawnerService : ISpawnerService
    {
        public SpawnerService(PanelSpawner panelSpawner, SitizenSpawner sitizenSpawner, ZombieSpawner zombieSpawner)
        {
            CurrentPanelSpawner = panelSpawner;
            CurrentSitizenSpawner = sitizenSpawner;
            CurrentZombieSpawner = zombieSpawner;
        }

        public PanelSpawner CurrentPanelSpawner { get; }
        public SitizenSpawner CurrentSitizenSpawner { get; }
        public ZombieSpawner CurrentZombieSpawner { get; }
    }
}
