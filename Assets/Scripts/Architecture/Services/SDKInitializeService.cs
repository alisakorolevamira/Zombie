using Agava.YandexGames;
using Cysharp.Threading.Tasks;
using System.Collections;

namespace Scripts.Architecture.Services
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