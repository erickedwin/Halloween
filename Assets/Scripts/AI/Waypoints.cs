using Nocturne.GeneralTools;
using System.Collections;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public Transform[] positions;

    private int selectedIndex;

    [SerializeField]
    private bool randomPath;

    [SerializeField]
    private EnemyAIStandard targetAI;

    [SerializeField]
    private float timeInterval = 4f;

    private void Start()
    {
        if (targetAI == null)
        {
            Debug.LogError("Asigna un AI que utilizara estoy waypoints");
        }

        if (positions.Length < 2)
        {
            Debug.LogError("Debe haber más de 1 posicion establecida para el waypoint");
        }

        selectedIndex = Random.Range(0, positions.Length - 1);
        targetAI.SetDestination(positions[selectedIndex]);
    }

    public void ChangeWaypoint()
    {
        if (targetAI.currentStatus == EnemyStatus.Patrolling)
        {
            StartCoroutine(ChangePosition());
        }
    }

    public void ChangeWaypoint(float time)
    {
        if (targetAI.currentStatus == EnemyStatus.Patrolling)
        {
            StartCoroutine(ChangePosition(time));
        }
    }

    public void QuitPatrol()
    {
        StopCoroutine(ChangePosition());
    }

    private IEnumerator ChangePosition()
    {
        yield return Helpers.GetWait(timeInterval);

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

    private IEnumerator ChangePosition(float time)
    {
        yield return Helpers.GetWait(time);

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