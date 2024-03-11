using Scripts.Architecture.Services;
using Scripts.Architecture.States;
using UnityEngine;

namespace Scripts.Architecture
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        private Game _game;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);

            _game = new Game(this);
            _game.StateMachine.Enter<BootstrapState>();
        }

        private void OnDestroy()
        {
            var focusService = AllServices.Container.Single<IFocusService>();

            focusService.Dispose();
        }
    }
}