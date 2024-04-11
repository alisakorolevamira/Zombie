using Constants.UI;
using DG.Tweening;
using UnityEngine;

namespace UI.Stars
{
    public class Star : MonoBehaviour
    {
        private void Start()
        {
            transform.DOScale(StarsConstants.MinimumStarSize, StarsConstants.MaximumStarSize).SetLoops(-1, LoopType.Yoyo);
        }

        private void OnDestroy()
        {
            DOTween.Kill(transform);
        }
    }
}