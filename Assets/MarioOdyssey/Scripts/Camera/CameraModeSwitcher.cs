using UnityEngine;
using Cinemachine;

public class CameraModeSwitcher : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _followCamera;
    [SerializeField] private CinemachineVirtualCamera _castCamera;
    [SerializeField] private HatThrower _hatThrower;

    private void OnEnable()
    {
        _hatThrower.Captured += OnCaptured;
    }

    private void OnDisable()
    {
        _hatThrower.Captured -= OnCaptured;
    }

    public void ActivateCastCamera()
    {
        _followCamera.gameObject.SetActive(false);

        if (_castCamera.TryGetComponent(out CinemachineInputProvider inputProvider))
            inputProvider.enabled = false;
    }

    public void DeactivateCastCamera()
    {
        _followCamera.gameObject.SetActive(true);

        if (_castCamera.TryGetComponent(out CinemachineInputProvider inputProvider))
            inputProvider.enabled = true;
    }

    public void OnCaptured(ICapturable capturable)
    {
        _followCamera.Follow = capturable.CameraTarget;
        _followCamera.LookAt = capturable.CameraTarget;
    }
}
