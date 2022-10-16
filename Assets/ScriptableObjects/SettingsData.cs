using System.Collections;
using System.Collections.Generic;
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

    //Change this, please
    public float resolution;

    public bool fullScreen;

    public bool runOnBackground;

    public bool vSyncEnabled;

    //TODO: make it an enum.
    public float frameRateLimit;

    public float FOV;

    //By default, the game will use DX11 (or Metal for Mac).
    //If we wanna add DX12 and/or Vulkan support for Windows/Mac/Linux, modify/use this attribute.
    public float renderer;

    public bool HDRDisplay;

    //IF we are going to use it. Else, just remove this field.
    public bool dynamicResolution;

    //URP supports AMD FidelityFX, so if you want to use it, i'll leave this field in here.
    public bool fidelityFXEnabled;

    [Header("Graphics")]
    public float textureQuality;

    public float shadowQuality;

    //Soft or Hard
    public float shadowType;

    public float anisotropicFiltering;

    //Will be: None, FXAA, MSAA, TAA
    public float antiAliasing;

    public float drawDistance;

    public bool bloomEnabled;

    public bool vignetteEnabled;

    public bool motionBlurEnabled;

    //DOF: Depth of Field
    public bool DOFEnabled;

    //SSAO: Screen Space Ambient Occlusion
    public bool SSAOEnabled;

    //SSR: Screen Space Reflections.
    //IMPORTANT NOTE: URP doesn't support SSR by default. If you want to implement it, we will have to wait until Unity implements it
    //, work with a workaround (i.e: use special shaders that simulate it), use Planar Reflections instead, or use an external asset that enables this.
    //If we don't need it, you can remove this attribute.
    public bool SSREnabled;

    [Header("Input")]
    public float sensitivityX;

    public float sensitivityY;

    public bool holdToSprint;

    public bool holdToCrouch;

    public void ResetSettings()
    {

    }

    public void DetectSettings()
    {

    }
}
