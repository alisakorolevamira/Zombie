namespace Scripts.Architecture.Services
{
    public interface ILevelDataService : IService
    {
        void LoadData();
        void ResetData();
        void SaveData();
        void UpdateData();
    }
}