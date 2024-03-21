using Scripts.Architecture.Services;
using Scripts.UI.Panels;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Buttons
{
    [RequireComponent(typeof(Image))]

    public class LoadGameButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private MenuPanel _menuPanel;

        private ISaveLoadService _saveLoadService;

        private void OnEnable()
        {
            _button.onClick.AddListener(_menuPanel.OpenProgressLevel);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(_menuPanel.OpenProgressLevel);
        }

        private void Start()
        {
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
            ChangeAviability();
        }

        private void ChangeAviability()
        {
            if (_saveLoadService.PlayerProgress.Level == Constants.Menu)
                _button.interactable = false;

            else
                _button.interactable = true;
        }
    }
}
