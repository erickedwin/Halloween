using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using DG.Tweening;
using Nocturne.GeneralTools;

public class LightSwitch : MonoBehaviour
{
    [SerializeField]
    private bool isOn;

    //TODO: add flicker
    [SerializeField]
    private bool canFlicker;

    [SerializeField]
    private Light[] lights;

    public static event Action<int> lightEnabled;

    private void Start()
    {
        if (isOn)
        {
            for (int i = 0; i < lights.Length; i++)
            {
                lights[i].enabled = true;
            }
        }
    }

    public void SwitchLightState()
    {
        if (isOn)
        {
            TurnOff();
        }
        else
        {
            TurnOn();
        }
    }

    private void TurnOn()
    {
        isOn = true;
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].enabled = true;
        }
    }

    private void TurnOff()
    {
        isOn = false;
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].enabled = false;
        }
    }

    public void StartFlicker()
    {

    }

    public void Flicker()
    {
        
    }

    public void StopFlicker()
    {

    }

    public bool GetState() => isOn;
}