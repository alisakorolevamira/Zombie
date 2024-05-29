using System.Collections;
using Constants.UI;
using UnityEngine;

namespace UI.Panels.Common
{
    public class LoadingPanel : Panel
    {
        public bool IsClosed { get; private set; }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public override void Open()
        {
            base.Open();
 
            IsClosed = false;
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
                WaitForSeconds fadeTime = new WaitForSeconds(UIConstants.FadeTime);

                yield return fadeTime;
            }

            _canvasGroup.blocksRaycasts = false;

            if (_image != null)
                _image.raycastTarget = false;

            IsClosed = true;
        }
    }
}