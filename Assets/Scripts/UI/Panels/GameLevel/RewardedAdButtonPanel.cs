using System.Collections;
using Constants.UI;
using UI.Panels.Common;
using UnityEngine;

namespace UI.Panels.GameLevel
{
    public class RewardedAdButtonPanel : Panel
    {
        public override void Open()
        {
            StartCoroutine(Fade());
        }

        public override void Close()
        {
            _canvasGroup.alpha = UIConstants.InvisibleAlfaIndex;
            _canvasGroup.blocksRaycasts = false;
        }

        private IEnumerator Fade()
        {
            while (_canvasGroup.alpha < UIConstants.VisibleAlfaIndex)
            {
                _canvasGroup.alpha += UIConstants.FadeCoefficient;
                WaitForSeconds fadeTime = new WaitForSeconds(AdConstants.FadeTime);

                yield return fadeTime;
            }

            _canvasGroup.blocksRaycasts = true;
        }
    }
}