using Scripts.UI.Panels;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Buttons
{
    public class NewGameButton : MonoBehaviour
    {
        private const string FirstLevel = "FirstLevel";

        [SerializeField] private Button _button;
        [SerializeField] private MenuPanel _menuPanel;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            _menuPanel.OpenAnyLevel(FirstLevel);
        }
    }
}