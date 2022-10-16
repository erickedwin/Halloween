using UnityEngine;
using UnityEngine.Events;

public class GeneralTrigger : MonoBehaviour
{
    [SerializeField]
    private UnityEvent OnTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnTrigger.Invoke();
        }
    }
}