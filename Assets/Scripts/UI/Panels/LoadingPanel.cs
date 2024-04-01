using Scripts.Constants;
using System.Collections;
using UnityEngine;

namespace Scripts.UI.Panels
{
    public class LoadingPanel : Panel
    {
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
            while (_canvasGroup.alpha > UIConstants.InvisibleAlfaIndex)
            {
                _canvasGroup.alpha -= UIConstants.FadeCoefficient;

                yield return new WaitForSeconds(UIConstants.FadeTime);
            }

            _canvasGroup.blocksRaycasts = false;

            if (_image != null)
                _image.raycastTarget = false;
        }
    }
}