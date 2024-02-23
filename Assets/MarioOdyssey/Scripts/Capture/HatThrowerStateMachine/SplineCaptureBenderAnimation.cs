using SplineMesh;
using System;
using UnityEngine;

public class SplineCaptureBenderAnimation : MonoBehaviour
{
    private const int FromPointIndex = 0;
    private const int ToPointIndex = 2;
    private const int TopPointIndex = 1;

    [SerializeField] private Spline _spline;
    [SerializeField] private ExampleContortAlong _contortAlong;
    [SerializeField] private float TopPointYOffset = 2;

    private Action _callback;

    private void OnValidate()
    {
        if(_spline.nodes.Count < ToPointIndex)
            throw new Exception(nameof(_spline));   
    }

    public void Play(Vector3 from, Vector3 to, Action callback)
    {
        _spline.gameObject.SetActive(true);

        _spline.nodes[FromPointIndex].Position = from;
        _spline.nodes[ToPointIndex].Position = to;

        _spline.nodes[TopPointIndex].Position = GetTopPointPositionBy(from, to);

        _callback = callback;

        _contortAlong.TimeIsOut += OnTimeIsOut;
    }

    private void OnTimeIsOut()
    {
        _spline.gameObject.SetActive(false);

        _callback?.Invoke();
        _callback = null;
    }

    private Vector3 GetTopPointPositionBy(Vector3 from, Vector3 to)
    {
        float x = GetAverageBetween(from.x, to.x);
        float z = GetAverageBetween(from.z, to.z);

        float y = from.y > to.y ? from.y + TopPointYOffset : to.y + TopPointYOffset;

        return new Vector3(x, y, z);
    }

    private float GetAverageBetween(float firstCoordinate, float secondCoordinate) => (firstCoordinate + secondCoordinate) / 2f;
}
