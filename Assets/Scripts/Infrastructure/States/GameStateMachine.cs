using Infrastructure.Logic;
using Services;
using Services.EnemySpawner;
using Services.Factory;
using Services.StaticData;
using System;
using System.Collections.Generic;
using UI.Services;

namespace Infrastructure.States
{
    public class GameStateMachine : IService
    {
        private readonly Dictionary<Type, IExitable> _states;
        private IExitable _activeState;

        public GameStateMachine(SceneLoader sceneLoader, AllServices services)
        {
            _states = new Dictionary<Type, IExitable>
            {
                [typeof(LoopState)] = new LoopState(),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader,
                    services.Single<UIFactory>(),
                    services.Single<StaticDataService>(),
                    services.Single<GameFactory>(),
                    services.Single<EnemySpawnerService>()),
            };
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }

        public void Enter<TState>() where TState : class, IState
        {
            TState state = ChangeState<TState>();
            state.Enter();
        }

        private TState ChangeState<TState>() where TState : class, IExitable
        {
            _activeState?.Exit();
            TState state = _states[typeof(TState)] as TState;
            _activeState = state;
            return state;
        }
    }
}