using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyAIStandard : MonoBehaviour
{
    [SerializeField]
    AIDestinationSetter targetAI;

    List<LightMinigame> ListLights;

    [SerializeField]
    float maxTimeSearch;

    float timeSearch;

    LightMinigame currentLightTarget;

    public Transform cacheLatestPatrolPosition { get; set; }

    public EnemyStatus currentStatus;

    private EnemyStatus previousStatus;
    
    void Start()
    {
        ListLights = new List<LightMinigame>();
        if (targetAI == null) targetAI = GetComponent<AIDestinationSetter>();
        currentStatus = EnemyStatus.Patrolling;
        previousStatus = EnemyStatus.Patrolling;
        timeSearch = maxTimeSearch;
    }

    public void DetectPlayer()
    {
        if (previousStatus != currentStatus) previousStatus = currentStatus;
        currentStatus = EnemyStatus.Attacking;
    }

    public void GetLightDetection(LightMinigame light)
    {
        if(previousStatus != currentStatus) previousStatus = currentStatus;

        ListLights.Add(light);
        if(currentLightTarget == null) currentLightTarget = light;

        if (currentStatus == EnemyStatus.Patrolling)
        {
            currentStatus = EnemyStatus.Deactivating;
            targetAI.target = light.transform;
        }
    }

    public void TurnOffLight()
    {
        if (currentLightTarget != null)
            currentLightTarget.TurnOff();
    }

    public void SetDestination(Transform destination)
    {
        cacheLatestPatrolPosition = destination;
        targetAI.target = destination;
    }

    public void DeleteLightDetection(LightMinigame light)
    {
        if (previousStatus != currentStatus) previousStatus = currentStatus;

        ListLights.Remove(light);

        if(ListLights.Count > 0)
        {
            //Go to the next light.
            if(targetAI.target == light.transform)
            {
                currentLightTarget = ListLights.FirstOrDefault();
                targetAI.target = currentLightTarget.transform;
            }
        }

        if(ListLights.Count == 0)
        {
            currentStatus = EnemyStatus.Patrolling;
            //Que vuelva a patrullar como antes.
            targetAI.target = cacheLatestPatrolPosition;
            currentLightTarget = null;
        }
    }

    private void Update()
    {
        if(currentStatus == EnemyStatus.Deactivating
            && currentLightTarget != null)
        {
            if(Vector3.Distance(gameObject.transform.position, currentLightTarget.gameObject.transform.position) < 5f)
            {
                //print("LLego a luz");
                TurnOffLight();
            }
        }
    }

    private void OnEnable()
    {
        LightsManager.OnTurnedOn += GetLightDetection;
        LightsManager.OnTurnedOff += DeleteLightDetection;
    }

    private void OnDisable()
    {
        LightsManager.OnTurnedOn -= GetLightDetection;
        LightsManager.OnTurnedOff -= DeleteLightDetection;
    }
}

public enum EnemyStatus
{
    Patrolling,
    Searching,
    Attacking,
    Deactivating
}
