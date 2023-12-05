namespace Scripts.Architecture.Services
{
    public interface ISaveLoadService : IService
    {
        int LoadProgress(string key, int value);
        string LoadProgress(string key, string value);
        void SaveProgress(string key, int value);
        void SaveProgress(string key, string value);
        void ResetProgress(string key);
    }
}
