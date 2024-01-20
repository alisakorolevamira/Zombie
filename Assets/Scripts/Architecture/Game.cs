using Scripts.Spawner;
using Scripts.Architecture.Services;
using Scripts.Architecture.States;
using UnityEngine;

namespace Scripts.Architecture
{
    public class Game
    {
        public GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner, GameObject spawner)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), AllServices.Container, spawner);

            spawner.GetComponent<PanelSpawner>().StateMachine = StateMachine;
        }
    }
}