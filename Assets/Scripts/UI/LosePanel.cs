using Scripts.Architecture.Services;
using Scripts.Spawner;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class LosePanel : Panel
    {
        [SerializeField] private Button _levelButton;
        [SerializeField] private Button _menuButton;

        private const string Menu = "Menu";

        private ISpawnerService _spawnerService;
        private SitizenSpawner _sitizenSpawner;
        private LevelPanel _levelPanel;
        private WinPanel _winPanel;

        private void OnEnable()
        {
            _spawnerService = AllServices.Container.Single<ISpawnerService>();
            _sitizenSpawner = _spawnerService.CurrentSitizenSpawner;
            _levelPanel = GetComponentInParent<LevelPanel>();

            _sitizenSpawner.AllSitizensDied += Open;
            _levelButton.onClick.AddListener(OnNextLevelButtonClick);
            _menuButton.onClick.AddListener(OnMenuButtonClick);

            Close();
        }

        private void OnDisable()
        {
            _sitizenSpawner.AllSitizensDied -= Open;
            _levelButton.onClick.RemoveListener(OnNextLevelButtonClick);
            _menuButton.onClick.RemoveListener(OnMenuButtonClick);
        }

        public override void Open()
        {
            base.Open();

            if (_spawnerService != null)
            {
                _winPanel = _spawnerService.CurrentPanelSpawner.GetPanel<WinPanel>();
                _winPanel.Close();
            }
        }

        private void OnNextLevelButtonClick()
        {
            _levelPanel.OpenSceneWithResetingProgress(SceneManager.GetActiveScene().name);
        }

        private void OnMenuButtonClick()
        {
            _levelPanel.OpenNextScene(Menu);
        }
    }
}
