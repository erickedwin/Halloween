using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using DG.Tweening;
using Nocturne.GeneralTools;
using UnityEngine.Events;

public class LightsManager : MonoBehaviour
{
    [SerializeField]
    int requiredLights;

    int lightsTurned;
    
    List<LightMinigame> lightSwitches;

    [SerializeField]
    UnityEvent OnTurnedOnAll;

    [SerializeField]
    UnityEvent OnTurnedOffAll;

    bool finished;

    public static event Action<int, LightSwitch> OnTurnedOn;

    public static event Action<int, LightSwitch> OnTurnedOff;

    void Start()
    {
        lightSwitches = new List<LightMinigame>();
        lightsTurned=0;
        finished = false;

    }
    
    public void LightOn(LightMinigame detectedLight)
    {
        lightsTurned++;
        if (lightsTurned >= requiredLights && !finished)
        {
            finished = true;
            OnTurnedOnAll.Invoke();
        }
    }

    public void LightOff(LightMinigame detectedLight)
    {
        lightsTurned--;
        if (lightsTurned < requiredLights && finished)
        {
            finished = false;
            OnTurnedOffAll.Invoke();
        }
    }

    private void OnEnable()
    {
        LightMinigame.TurnedOn += LightOn;
        LightMinigame.TurnedOff += LightOff;
    }

    private void OnDisable()
    {
        LightMinigame.TurnedOn -= LightOn;
        LightMinigame.TurnedOff -= LightOff;
    }

}
