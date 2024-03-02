using Agava.YandexGames;
using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Architecture
{
    public class SDKInitializer : MonoBehaviour
    {
        private async void Awake()
        {
            YandexGamesSdk.CallbackLogging = true;

            await Initialize().ToUniTask();

            SceneManager.LoadScene(Constants.Initial);
        }
        private IEnumerator Initialize()
        {
            yield return YandexGamesSdk.Initialize();
        }
    }
}
