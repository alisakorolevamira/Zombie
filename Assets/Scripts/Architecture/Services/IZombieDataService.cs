namespace Scripts.Architecture.Services
{
    public interface IZombieDataService : IService, IDataService
    {
        void ResetData();
    }
}