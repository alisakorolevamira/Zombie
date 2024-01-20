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

        private readonly int _menuIndex = 1;
        private readonly int _levelCoefficient = 1;

        private int _lastLevelIndex;
        private bool _isOpened;
        private ISpawnerService _spawnerService;
        private IZombieHealthService _zombieHealthService;
        private LevelPanel _levelPanel;
        private LosePanel _losePanel;
        private Image _image;
        private AudioSource _audioSource;

        private void OnEnable()
        {
            _zombieHealthService = AllServices.Container.Single<IZombieHealthService>();
            _spawnerService = AllServices.Container.Single<ISpawnerService>();
            _levelPanel = GetComponentInParent<LevelPanel>();
            _image = GetComponent<Image>();
            _audioSource = GetComponent<AudioSource>();
            _lastLevelIndex = SceneManager.sceneCountInBuildSettings - _levelCoefficient;

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
            if (!_isOpened)
            {
                base.Open();

                _image.raycastTarget = true;
                _audioSource.PlayOneShot(_audioSource.clip);

                if (_spawnerService != null)
                {
                    _losePanel = _spawnerService.CurrentPanelSpawner.GetPanel<LosePanel>();
                    _losePanel.Close();
                }

                _isOpened = true;
            }
        }

        public override void Close()
        {
            base.Close();

            _image.raycastTarget = false;
            _isOpened = false;
        }

        private void OnNextLevelButtonClick()
        {
            int activeScene = SceneManager.GetActiveScene().buildIndex;

            if (activeScene != _lastLevelIndex)
            {
                _levelPanel.OpenSceneWithResetingProgress(activeScene + _levelCoefficient);
            }

            else
            {
                _levelPanel.OpenSceneWithResetingProgress(_menuIndex);
            }
        }

        private void OnMenuButtonClick()
        {
            _levelPanel.OpenNextScene(1);
        }
    }
}