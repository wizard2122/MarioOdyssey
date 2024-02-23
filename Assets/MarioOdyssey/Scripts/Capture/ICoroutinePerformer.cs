using System.Collections;
using UnityEngine;

public interface ICoroutinePerformer
{
    Coroutine StartPerform(IEnumerator coroutine);

    void StopPerform(Coroutine coroutine);
}
