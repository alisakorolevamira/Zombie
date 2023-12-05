using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    [RequireComponent(typeof(CanvasGroup))]

    public abstract class Panel : MonoBehaviour
    {
        private readonly int _openIndex = 1;
        private readonly int _closeIndex = 0;
        private CanvasGroup _canvasGroup;
        private Button[] _buttons;

        public virtual void Open()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _buttons = GetComponentsInChildren<Button>();

            _canvasGroup.alpha = _openIndex;

            if (_buttons != null)
            {
                foreach (var button in _buttons)
                {
                    button.interactable = true;
                }
            }
        }

        public virtual void Close()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _buttons = GetComponentsInChildren<Button>();

            _canvasGroup.alpha = _closeIndex;

            if (_buttons != null)
            {
                foreach (var button in _buttons)
                {
                    button.interactable = false;
                }
            }
        }
    }
}
