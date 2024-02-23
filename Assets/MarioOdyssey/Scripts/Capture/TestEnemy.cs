using UnityEngine;
using Zenject;

[RequireComponent(typeof(CharacterMover))]
public class TestEnemy : MonoBehaviour, ICapturable, IPause
{
    [SerializeField] private CharacterMover _mover;

    private PauseHandler _pauseHandler;

    [field: SerializeField] public Transform HatRoot { get; private set; }

    [field: SerializeField] public Transform CameraTarget { get; private set; }

    public Transform Transform => transform;

    [Inject]
    private void Construct(CharacterInput input, PauseHandler pauseHandler)
    {
        _pauseHandler = pauseHandler;
        _pauseHandler.Add(this);
        _mover.Initialize(input);
    }

    private void OnValidate()
    {
        _mover ??= GetComponent<CharacterMover>();
    }

    private void OnDestroy()
    {
        _pauseHandler.Remove(this);
    }

    public void Capture()
    {
        _mover.StartWork();
    }

    public void Uncapture()
    {
        _mover.StopWork();
    }

    public void SetPause(bool isPause)
    {
        _mover.SetPause(isPause);
    }
}
