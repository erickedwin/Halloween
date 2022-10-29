using Cinemachine;
using UnityEngine;

public class AdjustSensitivity : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera fpsCamera;

    private CinemachinePOV cinemachinePOV;

    private void Awake()
    {
        if (fpsCamera == null) fpsCamera = GetComponent<CinemachineVirtualCamera>();
        cinemachinePOV = fpsCamera.GetCinemachineComponent<CinemachinePOV>();
    }

    public void ChangeSensitivityX(float x)
    {
        cinemachinePOV.m_HorizontalAxis.m_MaxSpeed = x;
    }

    public void ChangeSensitivityY(float y)
    {
        cinemachinePOV.m_VerticalAxis.m_MaxSpeed = y;
    }

    public void ChangePOV(int pov)
    {
        fpsCamera.m_Lens.FieldOfView = pov;
    }

    private void OnEnable()
    {
        SettingsManager.OnFOVChanged += ChangePOV;
        SettingsManager.OnSensitivtyXChanged += ChangeSensitivityX;
        SettingsManager.OnSensitivtyYChanged += ChangeSensitivityY;
    }

    private void OnDisable()
    {
        SettingsManager.OnFOVChanged -= ChangePOV;
        SettingsManager.OnSensitivtyXChanged -= ChangeSensitivityX;
        SettingsManager.OnSensitivtyYChanged -= ChangeSensitivityY;
    }
}