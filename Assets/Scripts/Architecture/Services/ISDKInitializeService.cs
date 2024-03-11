using Cysharp.Threading.Tasks;

namespace Scripts.Architecture.Services
{
    public interface ISDKInitializeService : IService
    {
        UniTask StartCoroutineAsUniTask();
    }
}