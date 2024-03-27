using Agava.YandexGames;
using Scripts.Architecture;
using Scripts.Architecture.Services;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scripts.UI.Panels
{
    public class WinPanel : Panel
    {
        private readonly bool _isGameStopped = false;

        [SerializeField] private Button _levelButton;
        [SerializeField] private Button _menuButton;
        [SerializeField] private LevelPanel _levelPanel;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private LosePanel _losePanel;
        [SerializeField] private Button[] _otherButtons;
        [SerializeField] private StarsView _starsView;

        private bool _isOpened;
        private IZombieHealthService _zombieHealthService;
        private IFocusService _focusService;
        private IAudioService _audioService;
        private ILevelService _sceneService;

        private void Start()
        {
            _zombieHealthService = AllServices.Container.Single<IZombieHealthService>();
            _focusService = AllServices.Container.Single<IFocusService>();
            _audioService = AllServices.Container.Single<IAudioService>();
            _sceneService = AllServices.Container.Single<ILevelService>();

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

                string activeSceneName = SceneManager.GetActiveScene().name;
                Level activeScene = _sceneService.FindLevelByName(activeSceneName);

                _sceneService.LevelComplite(activeScene);
                _starsView.AddStars(activeScene.AmountOfStars);

                _audioSource.PlayOneShot(_audioSource.clip);
                _losePanel.Close();


                foreach (var button in _otherButtons)
                    button.interactable = false;

                _isOpened = true;
            }
        }

        public override void Close()
        {
            base.Close();

            _starsView.RemoveAllStars();

            foreach (var button in _otherButtons)
                button.interactable = true;

            _isOpened = false;
        }

        private void OnNextLevelButtonClick()
        {
            Close();

            string activeScene = SceneManager.GetActiveScene().name;
            string nextScene = _sceneService.FindNextLevel(activeScene);

            InterstitialAd.Show(OnOpenCallBack, OnCloseCallBack);

            _levelPanel.OpenNextSceneWithResetingProgress(nextScene);
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
            _audioService.MuteAudio(_isGameStopped);
        }

        private void OnCloseCallBack(bool closed)
        {
            if (closed)
            {
                _focusService.IsGameStopped = false;

                _focusService.PauseGame(!_isGameStopped);
                _audioService.MuteAudio(!_isGameStopped);
            }
        }
    }
}