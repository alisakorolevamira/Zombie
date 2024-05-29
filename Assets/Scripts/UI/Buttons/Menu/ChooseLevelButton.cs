using Architecture.GameLevel;
using Architecture.Services;
using Architecture.ServicesInterfaces.GameLevel;
using TMPro;
using UI.Panels.Menu;
using UI.Stars;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Buttons.Menu
{
    public class ChooseLevelButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private string _levelName;
        [SerializeField] private MenuPanel _menuPanel;
        [SerializeField] private StarsView _starsView;
        [SerializeField] private Image _lock;
        [SerializeField] private TMP_Text _text;

        private ILevelService _levelService;
        private Level _level;

        private void OnEnable()
        {
            _button.onClick.AddListener(LoadLevel);
            _menuPanel.Opened += ChangeAvailability;
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(LoadLevel);
            _menuPanel.Opened -= ChangeAvailability;
        }

        private void Start()
        {
            _levelService = AllServices.Container.Single<ILevelService>();
            _level = _levelService.FindLevelByName(_levelName);
        }

        private void LoadLevel() => _menuPanel.OpenLevel(_levelName);

        private void ChangeAvailability()
        {
            _starsView.RemoveAllStars();

            if (_level.IsAvailable)
            {
                SetStars();

                _text.gameObject.SetActive(true);
                _lock.gameObject.SetActive(false);

                _button.interactable = true;
            }
            else
            {
                _text.gameObject.SetActive(false);
                _lock.gameObject.SetActive(true);

                _button.interactable = false;
            }
        }

        private void SetStars()
        {
            if (_level.IsCompleted)
                _starsView.AddStars(_level.AmountOfStars);
        }
    }
}