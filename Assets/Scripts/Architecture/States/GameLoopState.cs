using Agava.YandexGames;
using UnityEngine;

namespace Architecture.States
{
    public class GameLoopState : IState
    {
        private bool _isLoaded = false;
        public void Enter() 
        {
            if (_isLoaded)
                return;

            _isLoaded = true;
            YandexGamesSdk.GameReady();
        }

        public void Exit() { }
    }
}