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
    AIDestinationSetter targetAI;

    void Start()
    {
        if(positions.Length < 2)
        {
            Debug.LogError("Debe haber más de 1 posicion establecida para el waypoint");
        }

        selectedIndex = Random.Range(0, positions.Length - 1);
        targetAI.target = positions[selectedIndex];
    }
    
    public void ChangeWaypoint()
    {
        print("LLegado a destino");
        StartCoroutine(ChangePosition());
    }

    public void SetQuickWaypoint()
    {
        targetAI.target = positions[selectedIndex];
    }

    IEnumerator ChangePosition()
    {
        yield return Helpers.GetWait(4f);
        if (randomPath)
        {
            int newIndex = Random.Range(0, positions.Length - 1);
            selectedIndex = newIndex;
            targetAI.target = positions[selectedIndex];
        }
        else
        {
            selectedIndex++;
            if (selectedIndex >= positions.Length)
            {
                selectedIndex = 0;
            }
            targetAI.target = positions[selectedIndex];
        }
    }
}
