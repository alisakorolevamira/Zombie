using Scripts.Architecture.Services;
using Scripts.Architecture.States;

namespace Scripts.Architecture
{
    public class Game
    {
        public Game(ICoroutineRunner coroutineRunner)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), AllServices.Container);
        }

        public GameStateMachine StateMachine { get; private set; }
    }
}