using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Panels
{
    [RequireComponent(typeof(CanvasGroup))]

    public abstract class Panel : MonoBehaviour
    {
        private readonly int _openIndex = 1;
        private readonly int _closeIndex = 0;

        [SerializeField] private protected Image _image;
        [SerializeField] private protected CanvasGroup _canvasGroup;
        [SerializeField] private Button[] _buttons;

        public virtual void Open()
        {
            _canvasGroup.alpha = _openIndex;
            _canvasGroup.blocksRaycasts = true;

            if (_buttons != null)
            {
                foreach (var button in _buttons)
                    button.interactable = true;
            }

            if (_image != null)
                _image.raycastTarget = true;
        }

        public virtual void Close()
        {
            _canvasGroup.alpha = _closeIndex;
            _canvasGroup.blocksRaycasts = false;

            if (_buttons != null)
            {
                foreach (var button in _buttons)
                    button.interactable = false;
            }

            if (_image != null)
                _image.raycastTarget = false;
        }
    }
}
