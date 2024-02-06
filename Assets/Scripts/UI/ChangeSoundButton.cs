using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    [RequireComponent(typeof(Button))]

    public class ChangeSoundButton : MonoBehaviour
    {
        private readonly int _minimumVolume = 0;
        private readonly int _maximumVolume = 1;

        [SerializeField] private AudioSource [] _audioSources;
        private Button _button;


        private void OnEnable()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(ChangeVolume);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(ChangeVolume);
        }

        private void ChangeVolume()
        {
            foreach (var audioSource in _audioSources)
            {
                if (audioSource.volume == _maximumVolume)
                    audioSource.volume = _minimumVolume;

                else
                    audioSource.volume = _maximumVolume;
            }
        }
    }
}
