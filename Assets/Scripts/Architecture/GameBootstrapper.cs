using Agava.YandexGames;
using Scripts.Architecture.States;
using UnityEngine;

namespace Scripts.Architecture
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private GameObject _spawner;
        private Game _game;

        private void Awake()
        {
            YandexGamesSdk.GameReady();

            _game = new Game(this, _spawner);
            _game.StateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(gameObject);
        }
    }
}