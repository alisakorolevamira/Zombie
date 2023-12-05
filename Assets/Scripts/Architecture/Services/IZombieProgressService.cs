using Scripts.Progress;

namespace Scripts.Architecture.Services
{
    public interface IZombieProgressService : IService, ISavedProgress
    {
        ZombieProgress Progress { get; }
        int MaximumHealth { get; }
    }
}