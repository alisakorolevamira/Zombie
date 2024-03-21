using UnityEngine;
using DG.Tweening;

namespace Scripts.UI
{
    public class Star : MonoBehaviour
    {
        private void Start()
        {
            transform.DOScale(0, 2).SetLoops(-1, LoopType.Yoyo);
        }

        private void OnDestroy()
        {
            DOTween.Kill(transform);
        }
    }
}
