using Architecture.Services;
using Architecture.ServicesInterfaces.Data;
using Constants;
using UI.Panels.Menu;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Buttons.Menu
{
    public class NewGameButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private MenuPanel _menuPanel;

        private ISaveLoadService _saveLoadService;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }

        private void Start()
        {
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
        }

        private void OnButtonClick()
        {
            _saveLoadService.ResetAllProgress();
            _menuPanel.OpenLevel(LevelConstants.FirstLevel);
        }
    }
}