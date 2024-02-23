using System.Collections.Generic;

public class HatThrowerStatesProvider
{
    private HatThrowerStatesProvider(HatThrowerStateMachine stateMachine, List<IState> states, List<IStateTransition> transitions)
    {
        stateMachine.Initialize(states, transitions);
    }
}
