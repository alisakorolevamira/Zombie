namespace Scripts.Architecture.Services
{
    public interface ILevelDataService : IService, IDataService
    {
        void ResetData();
    }
}