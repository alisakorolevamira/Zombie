namespace Architecture.ServicesInterfaces.Data
{
    public interface IZombieDataService : IService, IDataService
    {
        void ResetData();
    }
}