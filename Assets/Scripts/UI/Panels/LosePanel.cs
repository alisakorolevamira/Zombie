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
        [SerializeField] private Button _levelButton;
        [SerializeField] private Button _menuButton;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private LevelPanel _levelPanel;
        [SerializeField] private WinPanel _winPanel;

        private bool _isOpened;
        private ISpawnerService _spawnerService;
        private SitizenSpawner _sitizenSpawner;

        private void OnDisable()
        {
            _sitizenSpawner.AllSitizensDied -= Open;
            _levelButton.onClick.RemoveListener(OnNextLevelButtonClick);
            _menuButton.onClick.RemoveListener(OnMenuButtonClick);
        }

        private void Start()
        {
            _spawnerService = AllServices.Container.Single<ISpawnerService>();
            _sitizenSpawner = _spawnerService.CurrentSitizenSpawner;

            _sitizenSpawner.AllSitizensDied += Open;
            _levelButton.onClick.AddListener(OnNextLevelButtonClick);
            _menuButton.onClick.AddListener(OnMenuButtonClick);

            Close();
        }

        public override void Open()
        {
            if (!_isOpened)
            {
                base.Open();

                _audioSource.PlayOneShot(_audioSource.clip);
                _winPanel.Close();

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
            Close();

            //InterstitialAd.Show(OnOpenCallBack, OnCloseCallBack);
            _levelPanel.OpenSceneWithResetingProgress(SceneManager.GetActiveScene().name);
        }

        private void OnMenuButtonClick()
        {
            Close();

            //InterstitialAd.Show(OnOpenCallBack, OnCloseCallBack);
            _levelPanel.OpenNextScene(Constants.Menu);
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
