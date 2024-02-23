using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThrowingState : IState
{
    private const float MinDistanceToBack = 0.1f;

    private readonly IStateSwitcher _stateSwitcher;

    private Hat _hat;
    private IThrower _thrower;

    private CharacterInput _input;

    private float _distance = 8;
    private float _duration = 0.5f;

    private ICoroutinePerformer _coroutinePerformer;
    private Coroutine _moveBack;

    private ThrowerStateMachineData _data;

    public ThrowingState(
        IStateSwitcher stateSwitcher, 
        Hat hat,
        IThrower thrower, 
        CharacterInput input, 
        ICoroutinePerformer coroutinePerformer, 
        ThrowerStateMachineData data)
    {
        _stateSwitcher = stateSwitcher;
        _hat = hat;
        _thrower = thrower;
        _input = input;
        _coroutinePerformer = coroutinePerformer;
        _data = data;
    }

    public void Enter()
    {
        _hat.StartRotate();
        _hat.transform.DOBlendableMoveBy(_thrower.Transform.forward * _distance, _duration);

        _input.HatThrower.Throw.started += OnThrowButtonPressed;
        _hat.Captured += OnCaptured;
    }

    public void Exit()
    {
        if (_moveBack != null)
            _coroutinePerformer.StopPerform(_moveBack);

        _hat.StopRotate();
        _hat.transform.DOKill();

        _input.HatThrower.Throw.started -= OnThrowButtonPressed;
        _hat.Captured -= OnCaptured;
    }

    private void OnThrowButtonPressed(InputAction.CallbackContext callback)
    {
        _moveBack = _coroutinePerformer.StartPerform(MoveBack());

        _input.HatThrower.Throw.started -= OnThrowButtonPressed;
    }

    private void OnCaptured(ICapturable capturable)
    {
        _data.Capturable = capturable;

        _stateSwitcher.SwitchState<CapturedState>();
    }

    private IEnumerator MoveBack()
    {
        Vector3 direction;

        _hat.transform.DOKill();

        do
        {
            direction = _thrower.HatRoot.transform.position - _hat.transform.position;

            _hat.transform.Translate(direction.normalized * 25 * Time.deltaTime, Space.World);

            yield return null;
        }
        while (direction.magnitude > MinDistanceToBack);

        _stateSwitcher.SwitchState<HatOnThrowerHeadState>();
    }
}
