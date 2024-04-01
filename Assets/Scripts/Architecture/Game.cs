using Scripts.Architecture.Services;
using Scripts.Architecture.States;
using UnityEngine;

namespace Scripts.Architecture
{
    public class Game
    {
        public Game(ICoroutineRunner coroutineRunner, AudioSource audioSource)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), AllServices.Container, audioSource);
        }

        public GameStateMachine StateMachine { get; private set; }
    }
}