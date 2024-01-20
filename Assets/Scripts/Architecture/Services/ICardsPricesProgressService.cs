using Scripts.Progress;

namespace Scripts.Architecture.Services
{
    public interface ICardsPricesProgressService : IService, ISavedProgress
    {
        CardsPricesProgress Progress { get; }
    }
}