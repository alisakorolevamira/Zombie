using Scripts.Progress;

namespace Scripts.Architecture.Services
{
    public interface ICardsPricesDataService : IService
    {
        CardsPricesProgress CardsPricesProgress { get; }

        void LoadData();
        void ResetData();
        void SaveData();
        void UpdateData();
    }
}