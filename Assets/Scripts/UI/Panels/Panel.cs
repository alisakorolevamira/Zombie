using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Panels
{
    [RequireComponent(typeof(CanvasGroup))]

    public abstract class Panel : MonoBehaviour
    {
        private readonly int _openIndex = 1;
        private readonly int _closeIndex = 0;

        private protected Image _image;
        private CanvasGroup _canvasGroup;
        private Button[] _buttons;

        public virtual void Open()
        {
            GetComponents();

            _canvasGroup.alpha = _openIndex;
            _canvasGroup.blocksRaycasts = true;
            _image.raycastTarget = true;

            if (_buttons != null)
            {
                foreach (var button in _buttons)
                    button.interactable = true;
            }
        }

        public virtual void Close()
        {
            GetComponents();

            _canvasGroup.alpha = _closeIndex;
            _canvasGroup.blocksRaycasts = false;
            _image.raycastTarget = false;

            if (_buttons != null)
            {
                foreach (var button in _buttons)
                    button.interactable = false;
            }
        }

        private void GetComponents()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _image = GetComponent<Image>();
            _buttons = GetComponentsInChildren<Button>();
        }
    }
}
