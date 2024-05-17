using System;
using System.Collections.Generic;
using Architecture.Services;
using Architecture.ServicesInterfaces;
using Architecture.ServicesInterfaces.Data;
using Architecture.ServicesInterfaces.UI;
using UnityEngine;

namespace Architecture.States
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;

        private IExitableState _activeState;

        public GameStateMachine(
            SceneLoader sceneLoader,
            AllServices services,
            AudioSource audioSource,
            ICoroutineRunner coroutineRunner)
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services, audioSource),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, services.Single<IUIPanelService>(), 
                services.Single<ISpawnerService>()),
                [typeof(LoadProgressState)] = new LoadProgressState(this, services.Single<ISaveLoadService>()),
                [typeof(GameLoopState)] = new GameLoopState(coroutineRunner, services.Single<IUIPanelService>())
            };
        }
        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayLoad>(TPayLoad payLoad) where TState : class, IPayLoadedState<TPayLoad>
        {
            TState state = ChangeState<TState>();
            state.Enter(payLoad);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();

            TState state = GetState<TState>();
            _activeState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState
        {
            return _states[typeof(TState)] as TState;
        }
    }
}