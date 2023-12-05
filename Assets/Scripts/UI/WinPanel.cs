using Scripts.Architecture.Services;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class WinPanel : Panel
    {
        [SerializeField] private Button _levelButton;
        [SerializeField] private Button _menuButton;

        private const string Menu = "Menu";
        private const string FirstLevel = "FirstLevel";
        private const string SecondLevel = "SecondLevel";
        private const string ThirdLevel = "ThirdLevel";
        private const string FourthLevel = "FourthLevel";
        private const string FifthLevel = "FifthLevel";

        private ISpawnerService _spawnerService;
        private IZombieHealthService _zombieHealthService;
        private LevelPanel _levelPanel;
        private LosePanel _losePanel;

        private void OnEnable()
        {
            _zombieHealthService = AllServices.Container.Single<IZombieHealthService>();
            _spawnerService = AllServices.Container.Single<ISpawnerService>();
            _levelPanel = GetComponentInParent<LevelPanel>();

            if (_zombieHealthService != null)
            {
                _zombieHealthService.Died += Open;
            }

            _levelButton.onClick.AddListener(OnNextLevelButtonClick);
            _menuButton.onClick.AddListener(OnMenuButtonClick);

            Close();
        }

        private void OnDisable()
        {
            if (_zombieHealthService != null)
            {
                _zombieHealthService.Died -= Open;
            }

            _levelButton.onClick.RemoveListener(OnNextLevelButtonClick);
            _menuButton.onClick.RemoveListener(OnMenuButtonClick);
        }

        public override void Open()
        {
            base.Open();

            if (_spawnerService != null)
            {
                _losePanel = _spawnerService.CurrentPanelSpawner.GetPanel<LosePanel>();
                _losePanel.Close();
            }
        }

        private void OnNextLevelButtonClick()
        {
            string activeScene = SceneManager.GetActiveScene().name;

            switch (activeScene)
            {
                case FirstLevel:
                    _levelPanel.OpenSceneWithResetingProgress(SecondLevel);
                    break;
                case SecondLevel:
                    _levelPanel.OpenSceneWithResetingProgress(ThirdLevel);
                    break;
                case ThirdLevel:
                    _levelPanel.OpenSceneWithResetingProgress(FourthLevel);
                    break;
                case FourthLevel:
                    _levelPanel.OpenSceneWithResetingProgress(FifthLevel);
                    break;
                case FifthLevel:
                    _levelPanel.OpenSceneWithResetingProgress(Menu);
                    break;
                default:
                    break;
            }
        }

        private void OnMenuButtonClick()
        {
            _levelPanel.OpenNextScene(Menu);
        }
    }
}