using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterView : MonoBehaviour
{
    private const string MovementDirectionKey = "MovementDirection";

    [SerializeField] private CharacterMover _characterMover;
    private Animator _animator;

    private void Awake() => _animator = GetComponent<Animator>();

    private void OnEnable()
    {
        _characterMover.MovementDirectionComputed += OnMovementDirectionComputed;
    }

    private void OnDisable()
    {
        _characterMover.MovementDirectionComputed -= OnMovementDirectionComputed;
    }

    private void OnMovementDirectionComputed(Vector3 movementDirection) => _animator.SetFloat(MovementDirectionKey, movementDirection.magnitude);

    public void Pause()
    {
        _animator.speed = 0;
    }
}
