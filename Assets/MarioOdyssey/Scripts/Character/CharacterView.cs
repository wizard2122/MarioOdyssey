using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(Animator))]
public class CharacterView : MonoBehaviour
{
    private const string MovementDirectionKey = "MovementDirection";

    [SerializeField] private CharacterMover _characterMover;
    [SerializeField] private VisualEffect _dissolveEffect;
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
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

    public void Pause() => _animator.speed = 0;

    public void Unpause() => _animator.speed = 1;

    public void OnCaptured()
    {
        Pause();
        _dissolveEffect.Play();
        _skinnedMeshRenderer.gameObject.SetActive(false);
    }

    public void OnUncaptured()
    {
        Unpause();
        _skinnedMeshRenderer.gameObject.SetActive(true);
    }
}
