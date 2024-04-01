using Scripts.Progress;

namespace Scripts.Architecture.Services
{
    public interface ICardsPricesDataService : IService, IDataService
    {
        CardsPricesProgress CardsPricesProgress { get; }

        void ResetData();
    }
}