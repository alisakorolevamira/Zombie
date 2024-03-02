using Scripts.Architecture.Services;
using Scripts.Architecture.States;
using Scripts.Spawner;
using Scripts.UI.Panels;
using UnityEngine;

namespace Scripts.Architecture
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private SitizenSpawner _sitizenSpawner;
        [SerializeField] private ZombieSpawner _zombieSpawner;
        [SerializeField] private LevelPanel _levelPanel;
        [SerializeField] private LoadingPanel _loadingPanel;

        private Game _game;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);

            _game = new Game(this, _sitizenSpawner, _zombieSpawner, _levelPanel, _loadingPanel);
            _game.StateMachine.Enter<BootstrapState>();
        }

        private void OnDestroy()
        {
            var focusService = AllServices.Container.Single<IFocusService>();

            focusService.Dispose();
        }
    }
}