using Agava.YandexGames;

namespace Architecture.States
{
    public class GameLoopState : IState
    {
        private bool _isReady;
        public void Enter() 
        {
            if (_isReady)
                return;

            _isReady = true;
            YandexGamesSdk.GameReady();
        }

        public void Exit() { }
    }
}