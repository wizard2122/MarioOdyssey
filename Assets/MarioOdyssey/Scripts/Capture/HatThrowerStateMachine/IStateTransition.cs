using System;

public interface IStateTransition 
{
    IState From { get; }
    IState To { get; }

    void Process(Action<IState> applyCurrentStateCallback);
}
