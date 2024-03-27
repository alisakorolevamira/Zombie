using Scripts.Progress;

namespace Scripts.Architecture.Services
{
    public interface IPlayerDataService : IService
    {
        PlayerProgress PlayerProgress { get; }

        void ResetData(string sceneName);
        void LoadData();
        void SaveData();
        void UpdateData();
    }
}