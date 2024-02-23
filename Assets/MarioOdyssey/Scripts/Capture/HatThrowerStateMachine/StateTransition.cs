using System;

public abstract class StateTransition: IStateTransition
{
    public StateTransition(IState from, IState to)
    {
        From = from;
        To = to;
    }

    public IState From { get; }
    public IState To { get; }
    
    public abstract void Process(Action<IState> applyCurrentStateCallback);
}
