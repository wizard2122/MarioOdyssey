using System;

public interface ICaptureNotifier
{
    event Action<ICapturable> Captured;
    event Action<ICapturable> Uncaptured;
}
