namespace Architecture.ServicesInterfaces.TimeScaleAndAudio
{
    public interface ITimeScaleService : IService
    {
        bool IsGameStopped { get; set; }
        void ChangeTimeScale(bool value);
    }
}