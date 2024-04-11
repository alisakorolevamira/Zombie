using Architecture.Services;
using Architecture.ServicesInterfaces.TimeScaleAndAudio;
using Architecture.ServicesInterfaces.Zombie;
using Architecture.States;
using UnityEngine;

namespace Architecture
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private AudioSource _audioSource;

        private Game _game;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);

            _game = new Game(this, _audioSource);
            _game.StateMachine.Enter<BootstrapState>();
        }

        private void OnDestroy()
        {
            var focusService = AllServices.Container.Single<IFocusService>();
            var combatService = AllServices.Container.Single<ICombatService>();

            focusService.Dispose();
            combatService.Dispose();
        }
    }
}