using Cysharp.Threading.Tasks;

namespace Scripts.Architecture.Services
{
    public interface ISDKInitializer : IService
    {
        UniTask RunCoroutineAsTask();
    }
}