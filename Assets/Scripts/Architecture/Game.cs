using Architecture.Services;
using Architecture.States;
using UnityEngine;

namespace Architecture
{
    public class Game
    {
        public Game(ICoroutineRunner coroutineRunner, AudioSource audioSource)
        {
            StateMachine = new GameStateMachine(
                new SceneLoader(coroutineRunner),
                AllServices.Container,
                audioSource,
                coroutineRunner);
        }

        public GameStateMachine StateMachine { get; private set; }
    }
}