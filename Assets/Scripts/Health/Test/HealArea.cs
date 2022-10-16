using UnityEngine;

public class HealArea : MonoBehaviour
{
    [SerializeField]
    private float Heal;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IHealthSystem>(out var health) && !health.AtFullHealth())
        {
            health.Heal(Heal);
        }
    }
}