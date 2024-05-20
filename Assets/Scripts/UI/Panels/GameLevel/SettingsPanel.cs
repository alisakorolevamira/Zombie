using Architecture.Services;
using Architecture.ServicesInterfaces.Data;
using Architecture.ServicesInterfaces.TimeScaleAndAudio;
using Constants;
using UI.Panels.Common;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Panels.GameLevel
{
    public class SettingsPanel : Panel
    {
        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _saveButton;
        [SerializeField] private LevelPanel _levelPanel;

        private ISaveLoadService _saveLoadService;
        private ITimeScaleService _timeScaleService;

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
            _timeScaleService = AllServices.Container.Single<ITimeScaleService>();

            Close();
        }

        public override void Open()
        {
            base.Open();
            
            _timeScaleService.Pause();
        }

        public override void Close()
        {
            base.Close();

            _timeScaleService.Continue();
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