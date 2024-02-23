using UnityEngine.InputSystem;

public class HatOnThrowerHeadState : IState
{
    private IStateSwitcher _stateSwitcher;

    private CharacterInput _input;
    private HatSetter _hatSetter;

    private IThrower _thrower;

    public HatOnThrowerHeadState(IStateSwitcher stateSwitcher, HatSetter hatSetter, IThrower thrower, CharacterInput input)
    {
        _input = input;
        _hatSetter = hatSetter;
        _stateSwitcher = stateSwitcher;
        _thrower = thrower;
    }

    public void Enter()
    {
        _hatSetter.SetTo(_thrower.HatRoot);

        _input.HatThrower.Throw.started += OnThrowButtonPressed;
    }

    public void Exit()
    {
        _hatSetter.ResetBining();

        _input.HatThrower.Throw.started -= OnThrowButtonPressed;
    }

    private void OnThrowButtonPressed(InputAction.CallbackContext callback) => _stateSwitcher.SwitchState<ThrowingState>();
}
