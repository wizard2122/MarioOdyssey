using UnityEngine;

public class FPSSetter : MonoBehaviour
{
    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 144;
    }
}
