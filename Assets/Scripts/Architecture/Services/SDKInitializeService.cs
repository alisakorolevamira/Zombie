using System.Collections;
using Agava.YandexGames;
using Architecture.ServicesInterfaces;
using Cysharp.Threading.Tasks;

namespace Architecture.Services
{
    public class SDKInitializeService : ISDKInitializeService
    {
        public async UniTask StartCoroutineAsUniTask()
        {
            YandexGamesSdk.CallbackLogging = true;

            await Initialize().ToUniTask();
        }

        private IEnumerator Initialize()
        {
            yield return YandexGamesSdk.Initialize();
        }
    }
}