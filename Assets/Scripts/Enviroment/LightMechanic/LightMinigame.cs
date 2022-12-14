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
            if(this.CompareTag("Generador")){
            for (int i = 0; i < lights.Length; i++)
            {
                if(i==0){
                lights[i].enabled = true;
                }else{
                lights[i].enabled = false;
                }
                
            }
            }else{
                for (int i = 0; i < lights.Length; i++)
            {
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
        if(this.CompareTag("Generador")){
        for (int i = 0; i < lights.Length; i++)
        {
            //lights[i].enabled = true;
            if(i==0){
                lights[i].enabled = false;
                }else{
                lights[i].enabled = true;
                }       
        } 
        }else{
            for (int i = 0; i < lights.Length; i++)
            {
            lights[i].enabled = true;
            }
        }
        
        TurnedOn?.Invoke(this);
    }

    public void TurnOff()
    {
        isOn = false;
        if(this.CompareTag("Generador")){
            for (int i = 0; i < lights.Length; i++)
        {
            
            //lights[i].enabled = false;
            if(i==0){
                lights[i].enabled = true;
                }else{
                lights[i].enabled = false;
                }
        } 
        } else {
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].enabled = false;
        }
        }
        
        
        TurnedOff?.Invoke(this);
    }

    public bool GetState() => isOn;
}
