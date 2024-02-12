using Cysharp.Threading.Tasks;
using Scripts.Progress;

namespace Scripts.Architecture.Services
{
    public interface ISaveLoadService : IService
    {
        PlayerProgress PlayerProgress { get; }
        ZombieProgress ZombieProgress { get; }
        CardsPricesProgress CardsPricesProgress { get; }

        UniTask LoadProgress();
        UniTask SaveProgress();
        void ResetProgress();
    }
}
