using UnityEngine;
using UnityEngine.UI;
using Scripts.Architecture.Services;
using Scripts.Constants;

namespace Scripts.UI.Panels
{
    public class SettingsPanel : Panel
    {
        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _saveButton;
        [SerializeField] private LevelPanel _levelPanel;

        private ISaveLoadService _saveLoadService;
        private IFocusService _focusService;

        private void OnEnable()
        {
            _menuButton.onClick.AddListener(OnMenuButtonClick);
            _saveButton.onClick.AddListener(OnSaveButtonClick);
        }

        private void OnDisable()
        {
            _menuButton.onClick.RemoveListener(OnMenuButtonClick);
            _saveButton.onClick.RemoveListener(OnSaveButtonClick);
        }

        private void Start()
        {
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
            _focusService = AllServices.Container.Single<IFocusService>();

            Close();
        }

        public override void Open()
        {
            base.Open();

            _focusService.IsGameStopped = true;
            _focusService.PauseGame(!_focusService.IsGameStopped);
        }

        public override void Close()
        {
            base.Close();

            _focusService.IsGameStopped = false;
            _focusService.PauseGame(!_focusService.IsGameStopped);
        }

        private void OnMenuButtonClick()
        {
            Close();
            _levelPanel.OpenNextScene(LevelConstants.Menu);
        }

        private void OnSaveButtonClick()
        {
            Close();
            _saveLoadService.SaveProgress();
        }
    }
}