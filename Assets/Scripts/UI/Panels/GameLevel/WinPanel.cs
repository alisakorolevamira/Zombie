using Architecture.GameLevel;
using Architecture.Services;
using Architecture.ServicesInterfaces.GameLevel;
using Architecture.ServicesInterfaces.Zombie;
using Audio;
using Constants;
using UI.Panels.Common;
using UI.Stars;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.Panels.GameLevel
{
    public class WinPanel : Panel
    {
        [SerializeField] private Button _levelButton;
        [SerializeField] private Button _menuButton;
        [SerializeField] private LevelPanel _levelPanel;
        [SerializeField] private LosePanel _losePanel;
        [SerializeField] private Button[] _otherButtons;
        [SerializeField] private StarsView _starsView;
        [SerializeField] private ShortEffectAudio _shortEffectAudio;

        private bool _isOpened;
        private IZombieHealthService _zombieHealthService;
        private ILevelService _levelService;

        private void Start()
        {
            _zombieHealthService = AllServices.Container.Single<IZombieHealthService>();
            _levelService = AllServices.Container.Single<ILevelService>();

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
                Level activeScene = _levelService.FindLevelByName(activeSceneName);

                _levelService.LevelComplete(activeScene);
                _starsView.AddStars(activeScene.AmountOfStars);
                _losePanel.Close();
                _shortEffectAudio.PlayOneShot();

                foreach (Button button in _otherButtons)
                    button.interactable = false;

                _isOpened = true;
            }
        }

        public override void Close()
        {
            base.Close();

            _starsView.RemoveAllStars();

            foreach (Button button in _otherButtons)
                button.interactable = true;

            _isOpened = false;
        }

        private void OnNextLevelButtonClick()
        {
            string activeScene = SceneManager.GetActiveScene().name;
            string nextScene = _levelService.FindNextLevel(activeScene);

            Close();
            _levelPanel.OpenNextSceneWithResetingProgress(nextScene);
        }

        private void OnMenuButtonClick()
        {
            Close();
            _levelPanel.OpenNextSceneWithResetingProgress(LevelConstants.Menu);
        }
    }
}