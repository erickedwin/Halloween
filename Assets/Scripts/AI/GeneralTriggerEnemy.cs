using UnityEngine;
using UnityEngine.Events;

public class GeneralTriggerEnemy : MonoBehaviour
{
    [SerializeField]
    private UnityEvent OnTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            OnTrigger.Invoke();
        }
    }
}
