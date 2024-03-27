using Scripts.Architecture;
using Scripts.Architecture.Services;
using Scripts.UI.Panels;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Buttons
{
    public class ChooseLevelButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private string _levelName;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private MenuPanel _menuPanel;
        [SerializeField] private StarsView _starsView;

        private ILevelService _levelService;
        private Level _level;

        private void OnEnable()
        {
            _button.onClick.AddListener(LoadLevel);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(LoadLevel);
        }

        private void Start()
        {
            _levelService = AllServices.Container.Single<ILevelService>();
            _level = _levelService.FindLevelByName(_levelName);

            Enable();
        }

        private void Enable()
        {
            if (_level.IsAvailable == true)
            {
                SetStars();
                _button.interactable = true;
            }

            else
                _button.interactable = false;
        }

        private void LoadLevel()
        {
            _audioSource.Stop();
            _menuPanel.OpenAnyLevel(_levelName);
        }

        private void SetStars()
        {
            if (_level.IsComplited)
                _starsView.AddStars(_level.AmountOfStars);
        }
    }
}
