using UnityEngine;

[RequireComponent(typeof(CharacterMover))]
public class TestEnemy : MonoBehaviour, ICapturable
{
    [SerializeField] private CharacterMover _mover;

    [field: SerializeField] public Transform HatRoot { get; private set; }

    [field: SerializeField] public Transform CameraTarget { get; private set; }

    private void OnValidate()
    {
        _mover ??= GetComponent<CharacterMover>();
    }

    public void Capture()
    {
        _mover.StartWork();
    }
}
