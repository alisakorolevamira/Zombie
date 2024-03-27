namespace Scripts.Architecture.Services
{
    public interface IZombieDataService : IService
    {
        void LoadData();
        void ResetData();
        void SaveData();
        void UpdateData();
    }
}