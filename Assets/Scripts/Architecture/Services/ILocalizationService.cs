namespace Scripts.Architecture.Services
{
    public interface ILocalizationService : IService
    {
        Localization Localization { get; }
        void Initialize();
    }
}