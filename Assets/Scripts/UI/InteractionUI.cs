using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class InteractionUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textInteraction;

    public void DetectedInteractable()
    {
        textInteraction.text = "Interactuar";
    }

    public void LeftInteractable()
    {
        textInteraction.text = "";
    }

    private void OnEnable()
    {
        InteractionSystem.OnDetectInteractable += DetectedInteractable;
        InteractionSystem.OnLeaveInteractable += LeftInteractable;
    }

    private void OnDisable()
    {
        InteractionSystem.OnDetectInteractable -= DetectedInteractable;
        InteractionSystem.OnLeaveInteractable -= LeftInteractable;
    }
}
