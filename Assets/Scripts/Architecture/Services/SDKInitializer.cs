using Agava.YandexGames;
using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;

namespace Scripts.Architecture.Services
{
    public class SDKInitializer : MonoBehaviour, ISDKInitializer
    {
        private async void Awake()
        {
            await RunCoroutineAsTask();
        }

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
