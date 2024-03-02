using System.Collections;
using UnityEngine;

namespace Scripts.UI.Panels
{
    public class LoadingPanel : Panel
    {
        private const float _fadeCoefficient = 0.03f;
        private const float _fadeTime = 0.03f;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public override void Close()
        {
            StartCoroutine(FadeIn());
        }

        private IEnumerator FadeIn()
        {
            while (_canvasGroup.alpha > 0)
            {
                _canvasGroup.alpha -= _fadeCoefficient;

                yield return new WaitForSeconds(_fadeTime);
            }

            _canvasGroup.blocksRaycasts = false;

            if (_image != null)
            {
                _image.raycastTarget = false;
            }
        }
    }
}
