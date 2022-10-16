using UnityEngine;

public class HealArea : MonoBehaviour
{
    [SerializeField]
    private float Heal;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IHealthSystem>(out var health) && !health.AtFullHealth())
        {
            health.Heal(Heal);
        }
    }
}