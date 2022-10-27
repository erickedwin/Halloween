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

    public Transform cacheLatestPatrolPosition { get; set; }

    public EnemyStatus currentStatus;

    private EnemyStatus previousStatus;
    
    void Start()
    {
        if(targetAI == null) targetAI = GetComponent<AIDestinationSetter>();
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

        if (currentStatus == EnemyStatus.Patrolling)
        {
            currentStatus = EnemyStatus.Deactivating;
            targetAI.target = light.transform;
        }
        
        
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
                targetAI.target = ListLights.FirstOrDefault().transform;
            }
        }

        if(ListLights.Count <= 0)
        {
            currentStatus = EnemyStatus.Patrolling;
            //Que vuelva a patrullar como antes.
            targetAI.target = cacheLatestPatrolPosition;
        }
    }

    private void Update()
    {
        
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
