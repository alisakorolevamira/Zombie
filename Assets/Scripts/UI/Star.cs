using UnityEngine;
using DG.Tweening;
using Scripts.Constants;

namespace Scripts.UI
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