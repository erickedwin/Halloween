using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Nocturne.GeneralTools;

public class FlickeringController : MonoBehaviour
{
    [SerializeField]
    private Light Light;

    [SerializeField]
    float minIntensity;

    [SerializeField]
    float maxIntensity;

    float timeDelay;

    [SerializeField]
    bool OnStart;
    
    void Start()
    {
        if(Light == null)
        {
            Light = GetComponent<Light>();
        }

        if (OnStart)
        {
            StartFlicker();
        }
    }
    [ContextMenu("Start Flicker")]
    public void StartFlicker()
    {
        StartCoroutine(Flicker());
    }

    private IEnumerator Flicker()
    {
        timeDelay = Random.Range(0.01f, 0.2f);
        Light.DOIntensity(minIntensity, timeDelay);
        yield return Helpers.GetWait(timeDelay);
        timeDelay = Random.Range(0.01f, 0.2f);
        Light.DOIntensity(maxIntensity, timeDelay);
        yield return Helpers.GetWait(timeDelay);
        StartCoroutine(Flicker());
    }

    [ContextMenu("Stop Flicker")]
    public void StopFlicker()
    {
        StopCoroutine(Flicker());
        Light.DOIntensity(maxIntensity, 0.01f);
    }
}
