using Scripts.Architecture.Services;
using Scripts.Architecture.States;
using Scripts.UI.Panels;
using Scripts.Spawner;

namespace Scripts.Architecture
{
    public class Game
    {
        public Game(ICoroutineRunner coroutineRunner, SitizenSpawner sitizenSpawner, ZombieSpawner zombieSpawner, 
            LevelPanel levelPanel, LoadingPanel loadingPanel, Localization localization)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), AllServices.Container, sitizenSpawner,
                zombieSpawner, levelPanel, loadingPanel, localization);
        }

        public GameStateMachine StateMachine { get; private set; }
    }
}