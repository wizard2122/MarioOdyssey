using System;
using UnityEngine;
using UnityEngine.VFX;

public class CapturedAnimationTransition : StateTransition
{
    private SplineCaptureBenderAnimation _benderAnimation;
    private VisualEffect _dissolveEffect;

    private IThrower _thrower;

    private ThrowerStateMachineData _data;

    private HatSetter _hatSetter;

    private PauseHandler _pauseHandler;

    public CapturedAnimationTransition(
        ThrowingState from,
        CapturedState to,
        HatSetter hatSetter,
        PauseHandler pauseHandler,
        SplineCaptureBenderAnimation benderAnimation,
        VisualEffect dissolveEffect,
        IThrower thrower, 
        ThrowerStateMachineData data)
        : base(from, to)
    {
        _dissolveEffect = dissolveEffect;
        _benderAnimation = benderAnimation;
        _thrower = thrower;
        _data = data;
        _hatSetter = hatSetter;
        _pauseHandler = pauseHandler;
    }

    public override void Process(Action<IState> applyCurrentStateCallback)
    {
        _pauseHandler.SetPause(true);

        _hatSetter.SetTo(_data.Capturable.HatRoot);

        _thrower.Transform.gameObject.SetActive(false);

        _dissolveEffect.SendEvent("OnPlay");
        _dissolveEffect.gameObject.SetActive(true); 
        

        _benderAnimation.Play(
            _thrower.CameraTarget.position,
            _data.Capturable.CameraTarget.position,
            () =>
            {
                _pauseHandler.SetPause(false);
                //_dissolveEffect.gameObject.SetActive(false);
                applyCurrentStateCallback?.Invoke(To);
            });
    }
}
