using Constants.UI;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Panels.Common
{
    [RequireComponent(typeof(CanvasGroup))]

    public abstract class Panel : MonoBehaviour
    {
        [SerializeField] private protected Image _image;
        [SerializeField] private protected CanvasGroup _canvasGroup;
        [SerializeField] private Button[] _buttons;

        public virtual void Open()
        {
            _canvasGroup.alpha = UIConstants.OpenIndex;
            _canvasGroup.blocksRaycasts = true;

            if (_buttons != null)
            {
                foreach (Button button in _buttons)
                    button.interactable = true;
            }

            if (_image != null)
                _image.raycastTarget = true;
        }

        public virtual void Close()
        {
            _canvasGroup.alpha = UIConstants.CloseIndex;
            _canvasGroup.blocksRaycasts = false;

            if (_buttons != null)
            {
                foreach (Button button in _buttons)
                    button.interactable = false;
            }

            if (_image != null)
                _image.raycastTarget = false;
        }
    }
}