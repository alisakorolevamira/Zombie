using Scripts.Architecture.Services;
using Scripts.UI.Panels;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Buttons
{
    [RequireComponent(typeof(Image))]

    public class LoadGameButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private MenuPanel _menuPanel;

        private IPlayerDataService _playerDataService;

        private void OnEnable()
        {
            _button.onClick.AddListener(_menuPanel.OpenProgressLevel);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(_menuPanel.OpenProgressLevel);
        }

        private void Start()
        {
            _playerDataService = AllServices.Container.Single<IPlayerDataService>();
            ChangeAviability();
        }

        private void ChangeAviability()
        {
            if (_playerDataService.PlayerProgress.Level == Constants.Menu || _playerDataService.PlayerProgress.Level == string.Empty)
                _button.interactable = false;

            else
                _button.interactable = true;
        }
    }
}
