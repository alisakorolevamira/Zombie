using Scripts.Progress;

namespace Scripts.Architecture.Services
{
    public interface ISaveLoadService : IService
    {
        PlayerProgress PlayerProgress { get; }
        ZombieProgress ZombieProgress { get; }
        CardsPricesProgress CardsPricesProgress { get; }

        void LoadProgress();
        void SaveProgress();
        void ResetProgress();
    }
}
