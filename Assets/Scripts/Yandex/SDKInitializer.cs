using Agava.YandexGames;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Yandex
{
    public class SDKInitializer : MonoBehaviour
    {
        private const int SceneIndex = 1;

        private void Awake()
        {
            YandexGamesSdk.CallbackLogging = true;
        }

        private IEnumerator Start()
        {
            yield return YandexGamesSdk.Initialize(OnInitialized);
        }

        private void OnInitialized()
        {
            SceneManager.LoadScene(SceneIndex);
        }
    }
}
