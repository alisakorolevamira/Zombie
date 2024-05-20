namespace Architecture.ServicesInterfaces.TimeScaleAndAudio
{
    public interface ITimeScaleService : IService
    {
        bool IsGameStopped { get; }
        void ChangeTimeScale(bool value);
        void Pause();
        void Continue();
    }
}