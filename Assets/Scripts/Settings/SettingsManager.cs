using BayatGames.SaveGameFree;
using PSX;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField]
    private SettingsData SettingsData;

    public static SettingsManager Instance;

    [Header("Menu UI")]
    [SerializeField]
    private Toggle vSyncChange;

    [SerializeField]
    private Toggle sprintChange;

    [SerializeField]
    private Toggle crouchChange;

    [SerializeField]
    private Slider fpsSlider;

    [SerializeField]
    private Slider fovSlider;

    [SerializeField]
    private Slider sensitivityXSlide;

    [SerializeField]
    private Slider sensitivityYSlide;

    [SerializeField]
    private TextMeshProUGUI textFPSValue;

    [SerializeField]
    private TextMeshProUGUI textFOVValue;

    [SerializeField]
    private TextMeshProUGUI textXValue;

    [SerializeField]
    private TextMeshProUGUI textYValue;

    //Events to react
    public static event Action<bool> OnCrouchTypeChanged;

    public static event Action<bool> OnSprintTypeChanged;

    public static event Action<int> OnFOVChanged;

    public static event Action<float> OnSensitivtyXChanged;

    public static event Action<float> OnSensitivtyYChanged;

    private void Awake()
    {
        Instance = this;
    }

    bool canSave;

    private void Start()
    {
        //Set also the menu
        SettingsData = SaveGame.Load("Settings", SettingsData);

        QualitySettings.vSyncCount = SettingsData.vSyncEnabled ? 1 : 0;
        if (!SettingsData.vSyncEnabled)
        {
            Application.targetFrameRate = SettingsData.frameRateLimit;
        }
        SetUI();
        Debug.Log("Cargado");
    }

    public void SetVSync(bool enableVSync)
    {
        SettingsData.vSyncEnabled = enableVSync;
        QualitySettings.vSyncCount = SettingsData.vSyncEnabled ? 1 : 0;
        fpsSlider.interactable = !SettingsData.vSyncEnabled;
        if (!SettingsData.vSyncEnabled)
        {
            Application.targetFrameRate = SettingsData.frameRateLimit;
        }
    }

    public void HoldToSprint(bool hold)
    {
        SettingsData.holdToSprint = hold;
        
        if (canSave)
        {
            OnSprintTypeChanged?.Invoke(hold);
            SaveSettings();
        }
    }

    public void HoldToCrouch(bool hold)
    {
        SettingsData.holdToCrouch = hold;
        
        if (canSave)
        {
            OnCrouchTypeChanged?.Invoke(hold);
            SaveSettings();
        }
    }

    public void SetSensitivityX(float sensitivity)
    {
        SettingsData.sensitivityX = sensitivity;
        textXValue.text = $"{sensitivity:0.00}";
        
        if (canSave)
        {
            OnSensitivtyXChanged?.Invoke(sensitivity);
            SaveSettings();
        }
    }

    public void SetSensitivityY(float sensitivity)
    {
        SettingsData.sensitivityY = sensitivity;
        textYValue.text = $"{sensitivity:0.00}";
        if (canSave)
        {
            OnSensitivtyYChanged?.Invoke(sensitivity);
            SaveSettings();
        }
    }

    public void SetFOV(float fov)
    {
        fov = (int)Mathf.Clamp(fov, 20, 100);
        SettingsData.FOV = fov;
        textFOVValue.text = $"{fov}";
        
        if (canSave)
        {
            OnFOVChanged?.Invoke((int)fov);
            SaveSettings();
        }
    }

    //We don't want to melt the PC with extremely high Frame Rate.
    public void SetFramerate(float target)
    {
        target = Mathf.Clamp(target, 20, 120);
        SettingsData.frameRateLimit = (int)target;
        Application.targetFrameRate = (int)target;
        textFPSValue.text = $"{target}";
        if (canSave)
        {
            SaveSettings();
        }
    }

    public void ResetSettings()
    {
        SettingsData.ResetSettings();
        SaveSettings();
        SetUI();
    }

    public void SetUI()
    {
        canSave = false;

        vSyncChange.isOn = SettingsData.vSyncEnabled;
        crouchChange.isOn = SettingsData.holdToCrouch;
        sprintChange.isOn = SettingsData.holdToSprint;
        fpsSlider.value = SettingsData.frameRateLimit;
        fovSlider.value = SettingsData.FOV;
        sensitivityYSlide.value = SettingsData.sensitivityY;
        sensitivityXSlide.value = SettingsData.sensitivityX;

        textFOVValue.text = $"{SettingsData.FOV}";
        textFPSValue.text = $"{SettingsData.frameRateLimit}";
        textXValue.text = $"{SettingsData.sensitivityX:0.00}";
        textYValue.text = $"{SettingsData.sensitivityY:0.00}";

        OnSprintTypeChanged?.Invoke(SettingsData.holdToSprint);
        OnCrouchTypeChanged?.Invoke(SettingsData.holdToCrouch);
        OnSensitivtyXChanged?.Invoke(SettingsData.sensitivityX);
        OnSensitivtyYChanged?.Invoke(SettingsData.sensitivityY);
        OnFOVChanged?.Invoke((int)SettingsData.FOV);

        canSave = true;
        SaveSettings();
    }

    public void SaveSettings()
    {
        //Save
        SaveGame.Save("Settings", SettingsData);
    }
}