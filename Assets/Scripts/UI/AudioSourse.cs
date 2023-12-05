using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof (AudioSourse))] 

public class AudioSourse : MonoBehaviour
{
    [SerializeField] private Button _button;
    private AudioSource _audioSource;
    private readonly int _minimumVolume = 0;
    private readonly int _maximumVolume = 1;

    private void OnEnable()
    {
        _button.onClick.AddListener(ChangeMusic);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(ChangeMusic);
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void ChangeMusic()
    {
        if (_audioSource != null)
        {
            if (_audioSource.volume == _maximumVolume)
            {
                _audioSource.volume = _minimumVolume;
            }

            else
            {
                _audioSource.volume = _maximumVolume;
            }
        }
    }
}
