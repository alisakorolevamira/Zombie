using Scripts.Progress;

namespace Scripts.Architecture.Services
{
    public interface IPlayerDataService : IService, IDataService
    {
        PlayerProgress PlayerProgress { get; }

        void ResetData(string sceneName);
    }
}