using UnityEngine;

namespace Scripts.Architecture.Services
{
    public class AudioService : IAudioService
    {
        private bool _isMuted = false;

        public void ChangeVolume(bool value)
        {
            if (_isMuted)
                return;

            AudioListener.volume = value ? Constants.MaximumVolumeValue : Constants.MinimumVolumeValue;
        }

        public void MuteAudio()
        {
            if (_isMuted)
            {
                _isMuted = false;
                ChangeVolume(!_isMuted);
            }

            else
            {
                ChangeVolume(_isMuted);
                _isMuted = true;
            }
        }
    }
}
