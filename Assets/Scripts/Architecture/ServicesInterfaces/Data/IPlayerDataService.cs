using Progress;

namespace Architecture.ServicesInterfaces.Data
{
    public interface IPlayerDataService : IService, IDataService
    {
        PlayerProgress PlayerProgress { get; }

        void ResetData(string sceneName);
    }
}