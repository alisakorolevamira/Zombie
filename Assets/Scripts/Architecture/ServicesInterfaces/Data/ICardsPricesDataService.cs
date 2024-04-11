using Progress;

namespace Architecture.ServicesInterfaces.Data
{
    public interface ICardsPricesDataService : IService, IDataService
    {
        CardsPricesProgress CardsPricesProgress { get; }

        void ResetData();
    }
}