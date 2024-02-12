using Scripts.Architecture.Services;
using Scripts.Spawner;
using Scripts.UI.Panels;
using System;
using System.Collections.Generic;

namespace Scripts.Architecture.States
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(SceneLoader sceneLoader, AllServices services, SitizenSpawner sitizenSpawner, ZombieSpawner zombieSpawner,
            LevelPanel levelPanel, LoadingPanel loadingPanel)
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services, sitizenSpawner, zombieSpawner, levelPanel, loadingPanel),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, services.Single<IUIPanelService>(), sitizenSpawner, zombieSpawner),
                [typeof(LoadProgressState)] = new LoadProgressState(this, services.Single<ISaveLoadService>()),
                [typeof(GameLoopState)] = new GameLoopState()
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