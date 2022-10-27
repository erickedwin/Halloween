using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nocturne.GeneralTools;
using Pathfinding;

public class Waypoints : MonoBehaviour
{
    public Transform[] positions;

    int selectedIndex;

    [SerializeField]
    bool randomPath;

    [SerializeField]
    EnemyAIStandard targetAI;

    void Start()
    {
        if(positions.Length < 2)
        {
            Debug.LogError("Debe haber más de 1 posicion establecida para el waypoint");
        }

        selectedIndex = Random.Range(0, positions.Length - 1);
        targetAI.SetDestination(positions[selectedIndex]);
    }
    
    public void ChangeWaypoint()
    {
        if(targetAI.currentStatus == EnemyStatus.Patrolling)
            StartCoroutine(ChangePosition());
    }

    public void QuitPatrol()
    {
        StopCoroutine(ChangePosition());
    }

    IEnumerator ChangePosition()
    {
        yield return Helpers.GetWait(4f);
        if (targetAI.currentStatus == EnemyStatus.Patrolling)
        {
            if (randomPath)
            {
                int newIndex = Random.Range(0, positions.Length - 1);
                selectedIndex = newIndex;
                targetAI.SetDestination(positions[selectedIndex]);
            }
            else
            {
                selectedIndex++;
                if (selectedIndex >= positions.Length)
                {
                    selectedIndex = 0;
                }
                targetAI.SetDestination(positions[selectedIndex]);
            }
        }
        

    }
}
