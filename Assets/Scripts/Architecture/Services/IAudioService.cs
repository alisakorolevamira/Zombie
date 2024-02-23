namespace Scripts.Architecture.Services
{
    public interface IAudioService : IService
    {
        void ChangeVolume(bool value);
        void MuteAudio();
    }
}