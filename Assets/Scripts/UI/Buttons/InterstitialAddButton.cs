using Agava.YandexGames;
using Scripts.Architecture.Services;
using Scripts.Constants;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Buttons
{
    public class InterstitialAddButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private IFocusService _focusService;
        private IAudioService _audioService;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }

        private void Start()
        {
            _focusService = AllServices.Container.Single<IFocusService>();
            _audioService = AllServices.Container.Single<IAudioService>();
        }

        private void OnButtonClick()
        {
            InterstitialAd.Show(OnOpenCallBack, OnCloseCallBack);
        }

        private void OnOpenCallBack()
        {
            _focusService.IsGameStopped = true;

            _focusService.PauseGame(AddConstants.GameStopped);
            _audioService.MuteAudio(AddConstants.GameStopped);
        }

        private void OnCloseCallBack(bool closed)
        {
            if (closed)
            {
                _focusService.IsGameStopped = false;

                _focusService.PauseGame(AddConstants.GameOn);
                _audioService.MuteAudio(AddConstants.GameOn);
            }
        }
    }
}