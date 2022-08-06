using System;
using System.Collections.Generic;

public class GameStateMachine
{
    private Dictionary<Type, IExitableState> _states;
    private IExitableState _activeState;

    public GameStateMachine(AllServices services, InputSystem _input)
    {
        _states = new Dictionary<Type, IExitableState>
        {
            [typeof(BootState)] = new BootState(services, _input),
            [typeof(LoadLevelState)] = new LoadLevelState(),
            [typeof(GameFlowState)] = new GameFlowState()
        };
    }

    public void Enter<TState>() where TState : class, IState
    {
        IState state = ChangeState<TState>();
        state.Enter();
    }

    private TState ChangeState<TState>() where TState : class, IExitableState
    {
        _activeState?.Exit();

        TState state = GetState<TState>();
        _activeState = state;

        return state;
    }

    private TState GetState<TState>() where TState : class, IExitableState =>
      _states[typeof(TState)] as TState;
}