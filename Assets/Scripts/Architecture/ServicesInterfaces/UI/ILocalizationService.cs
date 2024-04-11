using UI;

namespace Architecture.ServicesInterfaces.UI
{
    public interface ILocalizationService : IService
    {
        Localization Localization { get; }
        void Initialize();
    }
}