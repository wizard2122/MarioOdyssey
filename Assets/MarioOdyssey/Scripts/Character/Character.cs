using UnityEngine;

[RequireComponent(typeof(CharacterMover), typeof(HatThrower))]
public class Character : MonoBehaviour
{
    [SerializeField] private CharacterView _view;

    private CharacterMover _mover;
    private HatThrower _hatThrower;

    private void OnValidate()
    {
        _mover ??= GetComponent<CharacterMover>();
        _hatThrower ??= GetComponent<HatThrower>();
    }

    private void Awake()
    {
        _mover.StartWork();
    }

    private void OnEnable()
    {
        _hatThrower.Captured += OnCaptured;
    }

    private void OnDisable()
    {
        _hatThrower.Captured -= OnCaptured;
    }

    private void OnCaptured(ICapturable capturable)
    {
        _mover.StopWork();
        _view.Pause();
    }
}
