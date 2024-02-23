using System;
using UnityEngine;

public class Hat : MonoBehaviour
{
    public event Action<ICapturable> Captured;

    [SerializeField] private HatView _view;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out ICapturable capturable))
            Captured?.Invoke(capturable);
    }

    public void StartRotate() => _view.StartRotate();
    public void StopRotate() => _view.StopRotate();
}
