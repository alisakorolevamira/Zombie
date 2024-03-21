using Scripts.Architecture;
using Scripts.UI.Panels;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Buttons
{
    public class ChooseLevelButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private SceneSO _scene;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private MenuPanel _menuPanel;
        [SerializeField] private StarsView _starsView;

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
            Enable();
        }

        private void Enable()
        {
            if (_scene.IsAvailable == true)
            {
                SetStars();
                _button.interactable = true;
            }

            else
                _button.interactable = false;
        }

        private void LoadLevel()
        {
            _starsView.RemoveAllStars();
            _scene.ResetProgress();
            _audioSource.Stop();
            _menuPanel.OpenAnyLevel(_scene.Name);
        }

        private void SetStars()
        {
            if (_scene.IsComplited)
                _starsView.AddStars(_scene.AmountOfStars);
        }
    }
}
