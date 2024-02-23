using UnityEngine;

public interface IThrower : ITransformable
{
    Transform HatRoot { get; }
    Transform CameraTarget { get; }
}
