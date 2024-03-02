using Agava.YandexGames;
using Scripts.Architecture.Services;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scripts.UI.Panels
{
    public class LosePanel : Panel
    {
        private readonly bool _isGameStopped = false;

        [SerializeField] private Button _levelButton;
        [SerializeField] private Button _menuButton;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private LevelPanel _levelPanel;
        [SerializeField] private WinPanel _winPanel;

        private bool _isOpened;
        private ISpawnerService _spawnerService;
        private IFocusService _focusService;
        private IAudioService _audioService;

        private void OnDisable()
        {
            _spawnerService.SitizenSpawner.AllSitizensDied -= Open;
            _levelButton.onClick.RemoveListener(OnNextLevelButtonClick);
            _menuButton.onClick.RemoveListener(OnMenuButtonClick);
        }

        private void Start()
        {
            _spawnerService = AllServices.Container.Single<ISpawnerService>();
            _focusService = AllServices.Container.Single<IFocusService>();
            _audioService = AllServices.Container.Single<IAudioService>();

            _spawnerService.SitizenSpawner.AllSitizensDied += Open;
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

            InterstitialAd.Show(OnOpenCallBack, OnCloseCallBack);
            _levelPanel.OpenSceneWithResetingProgress(SceneManager.GetActiveScene().name);
        }

        private void OnMenuButtonClick()
        {
            Close();

            InterstitialAd.Show(OnOpenCallBack, OnCloseCallBack);
            _levelPanel.OpenNextScene(Constants.Menu);
        }

        private void OnOpenCallBack()
        {
            _focusService.IsGameStopped = true;
            _focusService.PauseGame(_isGameStopped);
            _audioService.ChangeVolume(_isGameStopped);
        }

        private void OnCloseCallBack(bool closed)
        {
            if (closed)
            {
                _focusService.IsGameStopped = false;
                _focusService.PauseGame(!_isGameStopped);
                _audioService.ChangeVolume(!_isGameStopped);
            }
        }
    }
}
