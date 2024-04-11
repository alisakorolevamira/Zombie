using Cysharp.Threading.Tasks;

namespace Architecture.ServicesInterfaces
{
    public interface ISDKInitializeService : IService
    {
        UniTask StartCoroutineAsUniTask();
    }
}