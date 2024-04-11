using UI.Panels.Common;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Panels.Menu
{
    public class MenuComponentPanel : Panel
    {
        [SerializeField] private Button [] _openButtons;
        [SerializeField] private Button [] _closeButtons;

        private void OnEnable()
        {
            foreach (var button in _openButtons)
                button.onClick.AddListener(Open);

            foreach (var button in _closeButtons)
                button.onClick.AddListener(Close);
        }

        private void OnDisable()
        {
            foreach (var button in _openButtons)
                button.onClick.RemoveListener(Open);

            foreach (var button in _closeButtons)
                button.onClick.RemoveListener(Close);
        }
    }
}