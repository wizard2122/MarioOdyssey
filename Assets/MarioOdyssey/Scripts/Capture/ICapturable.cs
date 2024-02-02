using UnityEngine;

public interface ICapturable
{
    Transform HatRoot { get; }
    Transform CameraTarget { get; }

    void Capture();
}
