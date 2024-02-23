using System;
using System.Collections.Generic;
using System.Linq;

public class HatThrowerStateMachine: IStateSwitcher
{
    private List<IState> _states;
    private List<IStateTransition> _transitions;
    private IState _currentState;

    public void Initialize(List<IState> states, List<IStateTransition> transitions)
    {
        _states = states;
        _transitions = transitions;

        _currentState = _states[0];
        _currentState.Enter();
    }

    public void SwitchState<T>() where T : IState
    {
        _currentState?.Exit();

        if(TryGetTransition<T>(out IStateTransition transition))
        {
            transition.Process(state =>
            {
                _currentState = state;
                _currentState.Enter();
            });

            return;
        }

        IState state = _states.FirstOrDefault(state => state is T);

        _currentState = state;
        _currentState.Enter();
    }

    private bool TryGetTransition<T>(out IStateTransition transition) where T : IState
    {
        transition = _transitions
            .FirstOrDefault(transition => 
            _currentState.GetType().IsAssignableFrom(transition.From.GetType()) && transition.To is T);

        return transition != null;
    }
}
