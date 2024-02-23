using UnityEngine;
using Zenject;

[RequireComponent(typeof(CharacterMover))]
public class Character : MonoBehaviour, IThrower, IPause
{
    [SerializeField] private CharacterView _view;
    [SerializeField] private Transform _cameraTarget;
    [SerializeField] private Transform _hatRoot;

    private CharacterMover _mover;
    private ICaptureNotifier _captureNotifier;

    private PauseHandler _pauseHandler;

    public Transform HatRoot => _hatRoot;

    public Transform CameraTarget => _cameraTarget;

    public Transform Transform => transform;

    [Inject]
    private void Construct(ICaptureNotifier captureNotifier, CharacterInput input, PauseHandler pauseHandler)
    {
        _captureNotifier = captureNotifier;
        _pauseHandler = pauseHandler;
        _pauseHandler.Add(this);
        _mover.Initialize(input);
    }

    private void OnValidate()
    {
        _mover ??= GetComponent<CharacterMover>();
    }

    private void Awake()
    {
        _mover.StartWork();
        _captureNotifier.Captured += OnCaptured;
        _captureNotifier.Uncaptured += OnUncaptured;
    }

    private void OnDestroy()
    {
        _captureNotifier.Captured -= OnCaptured;
        _captureNotifier.Uncaptured -= OnUncaptured;

        _pauseHandler.Remove(this);
    }

    private void OnCaptured(ICapturable capturable)
    {
        _mover.StopWork();
        gameObject.SetActive(false);
    }

    private void OnUncaptured(ICapturable capturable)
    {
        _mover.StartWork();
        _view.Unpause();
        gameObject.SetActive(true);
        //сделать прыжок и после прыжка startWork у мувера
    }

    public void SetPause(bool isPause)
    {
        _view.Pause();
        _mover.SetPause(isPause);
    }
}
