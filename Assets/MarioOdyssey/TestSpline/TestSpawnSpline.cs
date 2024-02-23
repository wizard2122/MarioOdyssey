using SplineMesh;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawnSpline : MonoBehaviour
{
    [SerializeField] private Spline _spline;

    [SerializeField] private Transform _point1;
    [SerializeField] private Transform _point2;

    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            _spline.gameObject.SetActive(true);
            _spline.nodes[0].Position = _point1.transform.position;
            _spline.nodes[2].Position = _point2.transform.position;

            _spline.GetComponent<ExampleContortAlong>().TimeIsOut += OnTimeIsOut;

            //_spline.nodes[0].Direction = new Vector3(_point1.transform.position.x, 3 + _point1.transform.position.y, 0);
            //_spline.nodes[1].Direction = new Vector3(_point2.transform.position.x, -3 + _point2.transform.position.y, 0);
        }
    }

    private void OnTimeIsOut()
    {
        _spline.gameObject.SetActive(false);
    }
}
