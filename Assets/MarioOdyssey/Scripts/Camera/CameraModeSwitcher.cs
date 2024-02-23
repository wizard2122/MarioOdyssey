using UnityEngine;
using Cinemachine;
using Zenject;

public class CameraModeSwitcher : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _followCamera;
    private ICaptureNotifier _captureNotifier;
    private IThrower _thrower;

    [Inject]
    private void Construct(ICaptureNotifier captureNotifier, IThrower thrower)
    {
        _thrower = thrower;
        _captureNotifier = captureNotifier;
    }

    private void OnEnable()
    {
        _captureNotifier.Captured += OnCaptured;
        _captureNotifier.Uncaptured += OnUncaptured;
    }

    private void OnDisable()
    {
        _captureNotifier.Captured -= OnCaptured;
        _captureNotifier.Uncaptured -= OnUncaptured;
    }

    public void OnCaptured(ICapturable capturable)
    {
        _followCamera.Follow = capturable.CameraTarget;
        _followCamera.LookAt = capturable.CameraTarget;
    }

    private void OnUncaptured(ICapturable capturable)
    {
        _followCamera.Follow = _thrower.CameraTarget;
        _followCamera.LookAt = _thrower.CameraTarget;
    }
}
