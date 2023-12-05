namespace Scripts.Progress
{
    public interface ISavedProgress
    {
        void LoadProgress();
        void SaveProgress();
        void ResetProgress();
    }
}
