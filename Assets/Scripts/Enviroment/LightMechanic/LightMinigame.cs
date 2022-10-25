using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using DG.Tweening;
using Nocturne.GeneralTools;

public class LightMinigame : MonoBehaviour
{
    [SerializeField]
    private bool isOn;

    [SerializeField]
    private Light[] lights;

    public static event Action<LightMinigame> TurnedOn;

    public static event Action<LightMinigame> TurnedOff;

    void Start()
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
        TurnedOn?.Invoke(this);
    }

    private void TurnOff()
    {
        isOn = false;
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].enabled = false;
        }
        TurnedOff?.Invoke(this);
    }

    public bool GetState() => isOn;
}
