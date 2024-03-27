namespace Scripts.Architecture.Services
{
    public interface ISaveLoadService : IService
    {
        void LoadProgress();
        void SaveProgress();
        void ResetProgress();
    }
}
