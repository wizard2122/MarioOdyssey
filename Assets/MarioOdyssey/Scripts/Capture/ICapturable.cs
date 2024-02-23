using UnityEngine;

public interface ICapturable: ITransformable
{
    Transform HatRoot { get; }
    Transform CameraTarget { get; }

    void Capture();
    void Uncapture();
}
