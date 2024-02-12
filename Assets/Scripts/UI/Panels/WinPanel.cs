using Agava.YandexGames;
using Scripts.Architecture.Services;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scripts.UI.Panels
{
    public class WinPanel : Panel
    {
        [SerializeField] private Button _levelButton;
        [SerializeField] private Button _menuButton;
        [SerializeField] private LevelPanel _levelPanel;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private LosePanel _losePanel;

        private bool _isOpened;
        private IZombieHealthService _zombieHealthService;

        private void Start()
        {
            _zombieHealthService = AllServices.Container.Single<IZombieHealthService>();

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
                _losePanel.Close();

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

            string activeScene = SceneManager.GetActiveScene().name;
            //InterstitialAd.Show(OnOpenCallBack, OnCloseCallBack);

            switch (activeScene)
            {
                case Constants.FirstLevel:
                    _levelPanel.OpenSceneWithResetingProgress(Constants.SecondLevel);
                    break;
                case Constants.SecondLevel:
                    _levelPanel.OpenSceneWithResetingProgress(Constants.ThirdLevel);
                    break;
                case Constants.ThirdLevel:
                    _levelPanel.OpenSceneWithResetingProgress(Constants.FourthLevel);
                    break;
                case Constants.FourthLevel:
                    _levelPanel.OpenSceneWithResetingProgress(Constants.FifthLevel);
                    break;
                case Constants.FifthLevel:
                    _levelPanel.OpenSceneWithResetingProgress(Constants.SixthLevel);
                    break;
                case Constants.SixthLevel:
                    _levelPanel.OpenSceneWithResetingProgress(Constants.SeventhLevel);
                    break;
                case Constants.SeventhLevel:
                    _levelPanel.OpenSceneWithResetingProgress(Constants.EightLevel);
                    break;
                case Constants.EightLevel:
                    _levelPanel.OpenSceneWithResetingProgress(Constants.NinthLevel);
                    break;
                case Constants.NinthLevel:
                    _levelPanel.OpenSceneWithResetingProgress(Constants.TenthLevel);
                    break;
                case Constants.TenthLevel:
                    _levelPanel.OpenSceneWithResetingProgress(Constants.EleventhLevel);
                    break;
                case Constants.EleventhLevel:
                    _levelPanel.OpenSceneWithResetingProgress(Constants.TwelthLevel);
                    break;
                case Constants.TwelthLevel:
                    _levelPanel.OpenSceneWithResetingProgress(Constants.Menu);
                    break;
            }
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