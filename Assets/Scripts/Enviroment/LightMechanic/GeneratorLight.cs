using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using DG.Tweening;
using Nocturne.GeneralTools;

public class GeneratorLight : MonoBehaviour
{
     [SerializeField]
    private bool isOn;

    [SerializeField]
    private Light[] lights;


    public static event Action<GeneratorLight> TurnedOn;

    public static event Action<GeneratorLight> TurnedOff;

    void Start()
    {
        if (isOn)
        {
            for (int i = 0; i < lights.Length; i++)
            {
                if(i==0){
                lights[i].enabled = false;
                }else{
                lights[i].enabled = true;
                }
                
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


    public void TurnOn()
    {
        isOn = true;
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].enabled = true;
        }
        TurnedOn?.Invoke(this);
    }

    public void TurnOff()
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
