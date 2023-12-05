using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Scripts.Architecture.Services;

namespace Scripts.UI
{
    public class SettingsPanel : Panel
    {
        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _saveButton;

        private const string Menu = "Menu";
        private readonly int _stopGameIndex = 0;
        private readonly int _startGameIndex = 1;

        private IPlayerProgressService _playerProgressService;
        private IZombieProgressService _zombieProgressService;

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
            Close();
        }

        public override void Open()
        {
            base.Open();

            gameObject.SetActive(true);
            Time.timeScale = _stopGameIndex;
        }

        public override void Close()
        {
            base.Close();

            gameObject.SetActive(false);
            Time.timeScale = _startGameIndex;
        }

        private void OnMenuButtonClick()
        {
            SceneManager.LoadScene(Menu);
        }

        private void OnSaveButtonClick()
        {
            _playerProgressService = AllServices.Container.Single<IPlayerProgressService>();
            _zombieProgressService = AllServices.Container.Single<IZombieProgressService>();

            _playerProgressService.SaveProgress();
            _zombieProgressService.SaveProgress();
        }
    }
}