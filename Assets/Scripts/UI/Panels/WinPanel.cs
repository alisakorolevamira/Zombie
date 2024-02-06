using Agava.YandexGames;
using Scripts.Architecture.Services;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scripts.UI.Panels
{
    public class WinPanel : Panel
    {
        private readonly int _menuIndex = 1;
        private readonly int _levelCoefficient = 1;

        [SerializeField] private Button _levelButton;
        [SerializeField] private Button _menuButton;

        private int _lastLevelIndex;
        private bool _isOpened;
        private ISpawnerService _spawnerService;
        private IZombieHealthService _zombieHealthService;
        private LevelPanel _levelPanel;
        private LosePanel _losePanel;
        private AudioSource _audioSource;

        private void OnEnable()
        {
            _zombieHealthService = AllServices.Container.Single<IZombieHealthService>();
            _spawnerService = AllServices.Container.Single<ISpawnerService>();

            _levelPanel = GetComponentInParent<LevelPanel>();
            _audioSource = GetComponent<AudioSource>();

            _lastLevelIndex = SceneManager.sceneCountInBuildSettings - _levelCoefficient;

            _zombieHealthService.Died += Open;
            _levelButton.onClick.AddListener(OnNextLevelButtonClick);
            _menuButton.onClick.AddListener(OnMenuButtonClick);

            Close();
        }

        private void OnDisable()
        {
            _zombieHealthService.Died -= Open;
            _levelButton.onClick.RemoveListener(OnNextLevelButtonClick);
            _menuButton.onClick.RemoveListener(OnMenuButtonClick);
        }

        public override void Open()
        {
            if (!_isOpened)
            {
                base.Open();

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

            _isOpened = false;
        }

        private void OnNextLevelButtonClick()
        {
            int activeScene = SceneManager.GetActiveScene().buildIndex;
            InterstitialAd.Show(OnOpenCallBack, OnCloseCallBack);

            if (activeScene != _lastLevelIndex)
                _levelPanel.OpenSceneWithResetingProgress(activeScene + _levelCoefficient);

            else
                _levelPanel.OpenSceneWithResetingProgress(_menuIndex);
        }

        private void OnMenuButtonClick()
        {
            InterstitialAd.Show(OnOpenCallBack, OnCloseCallBack);
            _levelPanel.OpenNextScene(1);
        }

        private void OnOpenCallBack()
        {
            Time.timeScale = 0;
            AudioListener.volume = 0f;
        }

        private void OnCloseCallBack(bool closed)
        {
            if (closed)
            {
                Time.timeScale = 1;
                AudioListener.volume = 1f;
            }
        }
    }
}