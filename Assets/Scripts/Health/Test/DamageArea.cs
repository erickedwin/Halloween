using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    [SerializeField]
    float Damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<IHealthSystem>(out var health) && !health.IsDead())
        {
            health.Damage(Damage);
        }
    }
}
