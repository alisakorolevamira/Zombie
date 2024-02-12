using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    [RequireComponent(typeof(Button))]

    public class ChangeSoundButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private void OnEnable()
        {
            _button.onClick.AddListener(ChangeVolume);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(ChangeVolume);
        }

        private void ChangeVolume()
        {
            if (AudioListener.volume == Constants.MaximumVolumeValue)
                AudioListener.volume = Constants.MinimumVolumeValue;

            else
                AudioListener.volume = Constants.MaximumVolumeValue;
        }
    }
}
