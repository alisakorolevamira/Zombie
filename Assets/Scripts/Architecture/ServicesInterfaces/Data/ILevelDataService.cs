namespace Architecture.ServicesInterfaces.Data
{
    public interface ILevelDataService : IService, IDataService
    {
        void ResetData();
    }
}