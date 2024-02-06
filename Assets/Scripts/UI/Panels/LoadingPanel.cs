using System.Collections;
using UnityEngine;

namespace Scripts.UI.Panels
{
    public class LoadingPanel : Panel
    {
        private const float _fadeCoefficient = 0.03f;
        private const float _fadeTime = 0.03f;
        private const int _openCanvasSortingOrder = 3;
        private const int _closeCanvasSortingOrder = 0;

        public override void Open()
        {
            base.Open();

            gameObject.SetActive(true);

            Canvas canvas = GetComponent<Canvas>();
            canvas.sortingOrder = _openCanvasSortingOrder;
        }

        public override void Close()
        {
            StartCoroutine(FadeIn());
        }

        private IEnumerator FadeIn()
        {
            CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
            Canvas canvas = GetComponent<Canvas>();

            while (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= _fadeCoefficient;

                yield return new WaitForSeconds(_fadeTime);
            }

            canvas.sortingOrder = _closeCanvasSortingOrder;
        }
    }
}
