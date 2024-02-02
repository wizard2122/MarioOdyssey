using DG.Tweening;
using UnityEngine;

public class HatView : MonoBehaviour
{
    [SerializeField] private float _durationOfOneCircle;

    public void StartRotate()
    {
        transform.DOLocalRotate(Vector3.up * 360f, _durationOfOneCircle, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
    }

    public void StopRotate()
    {
        transform.DOKill();
        transform.localRotation = Quaternion.identity;
    }
}
