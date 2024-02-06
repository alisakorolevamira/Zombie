using Agava.YandexGames;
using Scripts.Architecture.Services;
using Scripts.Spawner;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scripts.UI.Panels
{
    public class LosePanel : Panel
    {
        private readonly int _menuIndex = 1;

        [SerializeField] private Button _levelButton;
        [SerializeField] private Button _menuButton;

        private bool _isOpened;
        private ISpawnerService _spawnerService;
        private SitizenSpawner _sitizenSpawner;
        private LevelPanel _levelPanel;
        private WinPanel _winPanel;
        private AudioSource _audioSource;

        private void OnEnable()
        {
            _spawnerService = AllServices.Container.Single<ISpawnerService>();
            _sitizenSpawner = _spawnerService.CurrentSitizenSpawner;

            _levelPanel = GetComponentInParent<LevelPanel>();
            _image = GetComponent<Image>();
            _audioSource = GetComponent<AudioSource>();

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
            if (!_isOpened)
            {
                base.Open();

                _audioSource.PlayOneShot(_audioSource.clip);

                if (_spawnerService != null)
                {
                    _winPanel = _spawnerService.CurrentPanelSpawner.GetPanel<WinPanel>();
                    _winPanel.Close();
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
            InterstitialAd.Show(OnOpenCallBack, OnCloseCallBack);
            _levelPanel.OpenSceneWithResetingProgress(SceneManager.GetActiveScene().buildIndex);
        }

        private void OnMenuButtonClick()
        {
            InterstitialAd.Show(OnOpenCallBack, OnCloseCallBack);
            _levelPanel.OpenNextScene(_menuIndex);
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
