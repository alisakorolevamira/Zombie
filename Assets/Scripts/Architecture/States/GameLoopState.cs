using Agava.YandexGames;

namespace Scripts.Architecture.States
{
    public class GameLoopState : IState
    {
        public void Enter() 
        {
            YandexGamesSdk.GameReady();
        }

        public void Exit() { }
    }
}