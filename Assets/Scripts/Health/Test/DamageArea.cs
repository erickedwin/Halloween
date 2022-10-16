using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    [SerializeField]
    float Damage;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<IHealthSystem>(out var health) && !health.IsDead())
        {
            health.Damage(Damage);
        }
    }
}
