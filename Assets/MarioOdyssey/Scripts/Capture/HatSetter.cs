using UnityEngine;

public class HatSetter
{
    private Hat _hat;

    public HatSetter(Hat hat)
    {
        _hat = hat;
    }

    public void SetTo(Transform hatRoot)
    {
        if(_hat.transform.parent == hatRoot)
            return;

        _hat.transform.SetParent(hatRoot);
        _hat.transform.localPosition = Vector3.zero;
        _hat.transform.localRotation = Quaternion.identity;
    }

    public void ResetBining() => _hat.transform.SetParent(null);
}
