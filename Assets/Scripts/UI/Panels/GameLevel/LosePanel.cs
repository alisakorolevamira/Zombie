using Architecture.Services;
using Architecture.ServicesInterfaces.Zombie;
using Audio;
using Constants;
using UI.Panels.Common;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.Panels.GameLevel
{
    public class LosePanel : Panel
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _menuButton;
        [SerializeField] private LevelPanel _levelPanel;
        [SerializeField] private WinPanel _winPanel;
        [SerializeField] private Button[] _otherButtons;
        [SerializeField] private ShortEffectAudio _shortEffectAudio;

        private bool _isOpened;
        private ICombatService _combatService;

        private void OnDisable()
        {
            _combatService.AllCitizensDied -= Open;
            _restartButton.onClick.RemoveListener(OnRestartLevelButtonClick);
            _menuButton.onClick.RemoveListener(OnMenuButtonClick);
        }

        private void Start()
        {
            _combatService = AllServices.Container.Single<ICombatService>();

            _combatService.AllCitizensDied += Open;
            _restartButton.onClick.AddListener(OnRestartLevelButtonClick);
            _menuButton.onClick.AddListener(OnMenuButtonClick);

            Close();
        }

        public override void Open()
        {
            if (!_isOpened)
            {
                base.Open();

                _shortEffectAudio.PlayOneShot();
                _winPanel.Close();

                foreach (var button in _otherButtons)
                    button.interactable = false;

                _isOpened = true;
            }
        }

        public override void Close()
        {
            base.Close();

            foreach (var button in _otherButtons)
                button.interactable = true;

            _isOpened = false;
        }

        private void OnRestartLevelButtonClick()
        {
            Close();
            _levelPanel.OpenNextSceneWithResetingProgress(SceneManager.GetActiveScene().name);
        }

        private void OnMenuButtonClick()
        {
            Close();
            _levelPanel.OpenNextScene(LevelConstants.Menu);
        }
    }
}