using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "Scriptable Objects/Setting System")]
[System.Serializable]
public class SettingsData : ScriptableObject
{
    //For those who are reading this: this is a work-in-progress version of the settings
    //There will be new features and overhauls as the project progresses
    //You can modify it as much as you want.
    //... and yeah, this is my first time with a more complex settings system. Until now, i only worried about sounds and buttons settings.
    [Header("Audio")]
    public float volumeMusic;

    public float volumeEffects;

    public float volumeVoice;

    [Header("Display")]
    public float resolution;

    public bool fullScreen = true;

    public bool runOnBackground = false;

    public bool vSyncEnabled = true;

    //TODO: make it an enum.
    public float frameRateLimit = 60;

    public float FOV = 60;

    [Header("Graphics")]
    public bool enabledShadow = true;

    public float drawDistance;

    [Header("Input")]
    public float sensitivityX;

    public float sensitivityY;

    public bool holdToSprint = false;

    public bool holdToCrouch = false;

    public void ResetSettings()
    {
        fullScreen = true;

        runOnBackground = false;

        FOV = 60;

        frameRateLimit = 60;

        enabledShadow = true;

        holdToSprint = false;

        holdToCrouch = false;
    }

    public void DetectSettings()
    {
    }
}