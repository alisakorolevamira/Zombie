namespace Architecture.ServicesInterfaces.TimeScaleAndAudio
{
    public interface ITimeScaleService : IService
    {
        void Pause();
        void Continue();
    }
}