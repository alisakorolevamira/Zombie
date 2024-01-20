using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Architecture
{
    public class SceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void Load(int sceneIndex, Action onLoaded = null)
        {
            _coroutineRunner.StartCoroutine(LoadScene(sceneIndex, onLoaded));
        }

        private IEnumerator LoadScene(int nextSceneIndex, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().buildIndex == nextSceneIndex)
            {
                onLoaded?.Invoke();
                yield break;
            }

            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextSceneIndex);

            while (!waitNextScene.isDone)
            {
                yield return null;
            }

            onLoaded?.Invoke();
        }
    }
}
