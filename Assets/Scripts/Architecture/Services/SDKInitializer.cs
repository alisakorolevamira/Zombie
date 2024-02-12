using Agava.YandexGames;
using Cysharp.Threading.Tasks;
using System.Collections;

namespace Scripts.Architecture.Services
{
    public class SDKInitializer : ISDKInitializer
    {
        public async UniTask RunCoroutineAsTask()
        {
            YandexGamesSdk.CallbackLogging = true;

            await Start().ToUniTask();
        }

        private IEnumerator Start()
        {
            yield return YandexGamesSdk.Initialize();
        }
    }
}
