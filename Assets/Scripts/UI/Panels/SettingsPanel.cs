using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Scripts.Architecture.Services;

namespace Scripts.UI.Panels
{
    public class SettingsPanel : Panel
    {
        private readonly int _stopGameIndex = 0;
        private readonly int _startGameIndex = 1;

        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _saveButton;

        private ISaveLoadService _saveLoadService;

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

            Close();
        }

        public override void Open()
        {
            base.Open();

            Time.timeScale = _stopGameIndex;
        }

        public override void Close()
        {
            base.Close();

            Time.timeScale = _startGameIndex;
        }

        private void OnMenuButtonClick()
        {
            SceneManager.LoadScene(Constants.Menu);
        }

        private void OnSaveButtonClick()
        {
            _saveLoadService.SaveProgress();
        }
    }
}