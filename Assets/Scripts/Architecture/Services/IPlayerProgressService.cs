using Scripts.Progress;

namespace Scripts.Architecture.Services
{
    public interface IPlayerProgressService : ISavedProgress, IService
    {
        PlayerProgress Progress { get; }
    }
}