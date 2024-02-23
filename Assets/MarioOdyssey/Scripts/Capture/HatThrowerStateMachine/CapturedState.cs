using System;
using UnityEngine.InputSystem;

public class CapturedState : IState, ICaptureNotifier
{
    private IStateSwitcher _stateSwitcher;

    public event Action<ICapturable> Captured;
    public event Action<ICapturable> Uncaptured;

    private CharacterInput _input;

    private HatSetter _hatSetter;

    private ThrowerStateMachineData _data;

    public CapturedState(
        IStateSwitcher stateSwitcher, 
        CharacterInput input, 
        HatSetter hatSetter,
        ThrowerStateMachineData data) 
    {
        _stateSwitcher = stateSwitcher;
        _data = data;
        _input = input;
        _hatSetter = hatSetter;
    }

    public void Enter()
    {
        _input.HatThrower.Throw.started += OnThrowButtonPressed;

        _hatSetter.SetTo(_data.Capturable.HatRoot);

        _data.Capturable.Capture();
        Captured?.Invoke(_data.Capturable);
    }

    public void Exit()
    {
        _input.HatThrower.Throw.started -= OnThrowButtonPressed;

        _data.Capturable.Uncapture();
        _hatSetter.ResetBining();
        Uncaptured?.Invoke(_data.Capturable);
    }

    private void OnThrowButtonPressed(InputAction.CallbackContext callback) => _stateSwitcher.SwitchState<HatOnThrowerHeadState>();
}
