namespace Architecture.ServicesInterfaces.Data
{
    public interface ISaveLoadService : IService
    {
        void LoadProgress();
        void SaveProgress();
        void ResetAllProgress();
        void ResetProgressForNextLevel(string sceneName);
    }
}