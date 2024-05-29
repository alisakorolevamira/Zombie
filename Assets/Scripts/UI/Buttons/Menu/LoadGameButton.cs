using Architecture.Services;
using Architecture.ServicesInterfaces.Data;
using Constants;
using UI.Panels.Menu;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Buttons.Menu
{
    [RequireComponent(typeof(Image))]

    public class LoadGameButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private MenuPanel _menuPanel;

        private IPlayerDataService _playerDataService;

        private void OnEnable()
        {
            _button.onClick.AddListener(OpenProgressLevel);
            _menuPanel.Opened += ChangeAvailability;
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OpenProgressLevel);
            _menuPanel.Opened -= ChangeAvailability;
        }

        private void Start()
        {
            _playerDataService = AllServices.Container.Single<IPlayerDataService>();
        }

        private void ChangeAvailability()
        {
            if (
                _playerDataService.PlayerProgress.Level == LevelConstants.Menu ||
                string.IsNullOrEmpty(_playerDataService.PlayerProgress.Level))
                _button.interactable = false;
            else
                _button.interactable = true;
        }

        private void OpenProgressLevel() => _menuPanel.OpenLevel(_playerDataService.PlayerProgress.Level);
    }
}