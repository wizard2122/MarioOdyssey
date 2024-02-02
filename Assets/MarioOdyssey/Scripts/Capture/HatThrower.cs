using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatThrower : MonoBehaviour
{
    public event Action<ICapturable> Captured;

    private const float MinDistanceToBack = 0.05f;

    [SerializeField] private Hat _hat;
    [SerializeField, Range(2, 10)] private float _distance;
    [SerializeField, Range(0.5f, 3f)] private float _duration;
    [SerializeField] private Transform _hatRoot;

    private bool _isHatThrowed;
    private bool _isHatCaptured;
    private bool _isHatReturned = true;

    private Coroutine _moveBack;

    private void OnEnable()
    {
        _hat.Captured += OnCaptured;
    }

    private void OnDisable()
    {
        _hat.Captured -= OnCaptured;
    }

    private void OnCaptured(ICapturable capturable)
    {
        _hat.transform.DOKill();

        if (_moveBack != null)
            StopCoroutine(_moveBack);

        _isHatCaptured = true;

        _hat.StopRotate();
        SetHatTo(capturable.HatRoot);

        Captured?.Invoke(capturable);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && (_isHatThrowed || _isHatCaptured))
            Return();

        if (Input.GetKeyDown(KeyCode.F) && _isHatReturned)
            Throw();
    }

    private void Return()
    {
        _moveBack = StartCoroutine(MoveBack());
    }

    private IEnumerator MoveBack()
    {
        Vector3 direction;

        if (_isHatCaptured)
            _hat.StartRotate();

        _isHatThrowed = false;
        _isHatCaptured = false;
        _hat.transform.DOKill();

        do
        {
            direction = _hatRoot.transform.position - _hat.transform.position;

            _hat.transform.Translate(direction.normalized * 25 * Time.deltaTime, Space.World);

            yield return null;
        }
        while (direction.magnitude > MinDistanceToBack);

        _hat.StopRotate();
        _isHatReturned = true;
        SetHatTo(_hatRoot);
    }

    public void Throw()
    {
        _hat.transform.SetParent(null);
        _hat.StartRotate();
        _hat.transform.DOBlendableMoveBy(transform.forward * _distance, _duration);
        _isHatThrowed = true;
        _isHatReturned = false;
    }

    private void SetHatTo(Transform hatRoot)
    {
        _hat.transform.SetParent(hatRoot);
        _hat.transform.localPosition = Vector3.zero;
        _hat.transform.localRotation = Quaternion.identity;
    }
}
