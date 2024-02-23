using DG.Tweening;
using System;

public class UncapturedAnimationTransition : StateTransition
{
    private HatSetter _hatSetter;
    private IThrower _thrower;
    private ThrowerStateMachineData _data;

    public UncapturedAnimationTransition(CapturedState from, HatOnThrowerHeadState to, HatSetter hatSetter, IThrower thrower, ThrowerStateMachineData data) : base(from, to)
    {
        _hatSetter = hatSetter;
        _thrower = thrower;
        _data = data;
    }

    public override void Process(Action<IState> applyCurrentStateCallback)
    {
        _hatSetter.SetTo(_thrower.HatRoot);
        _thrower.Transform.gameObject.SetActive(true);

        _thrower.Transform.position = _data.Capturable.Transform.position + _data.Capturable.Transform.forward;

        _thrower.Transform.DOJump(_data.Capturable.Transform.position + _data.Capturable.Transform.forward * 5, 2, 1, 0.4f)
            .OnComplete(() => applyCurrentStateCallback?.Invoke(To));
    }
}
