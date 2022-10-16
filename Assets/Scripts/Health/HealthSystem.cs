using System;
using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour, IHealthSystem
{
    [SerializeField]
    private float maximumHealth;

    [SerializeField]
    private float startingHealth;

    [SerializeField]
    private UnityEvent OnDeath;

    [SerializeField]
    private UnityEvent OnResurrect;

    private float maxHealth;
    private float currentHealth;

    public event Action OnHealthChanged;

    public event Action OnDamage;

    public event Action OnHeal;

    public event Action OnMaxHealthChanged;

    //For 3D games, change this to a standard collider or charactercontroller, depending on what you use
    private BoxCollider2D _characterCollider;

    private void Awake()
    {
        maxHealth = maximumHealth;
        if (startingHealth > 0)
        {
            SetHealth(startingHealth);
        }
        else
        {
            currentHealth = maximumHealth;
        }

        _characterCollider = GetComponent<BoxCollider2D>();
    }

    //Para las pruebas

    public void Damage(float damage)
    {
        if (damage <= 0 || IsDead()) return;

        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        OnHealthChanged?.Invoke();
        if (currentHealth <= 0)
        {
            Death();
        }
        else
        {
            //Just damage
            OnDamage?.Invoke();
        }
    }

    public void Heal(float heal)
    {
        if (heal <= 0 || IsDead()) return;
        currentHealth = Mathf.Clamp(currentHealth + heal, 0, maxHealth);
        OnHealthChanged?.Invoke();
        OnHeal?.Invoke();
    }

    public void SetMaxHealth(float health)
    {
        float temp = maxHealth;
        maxHealth = health;
        OnMaxHealthChanged?.Invoke();
        if (temp <= currentHealth || currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
            OnHealthChanged?.Invoke();
        }
    }

    public float GetMaxHealth() => maxHealth;

    public void SetHealth(float newHealth)
    {
        currentHealth = newHealth;
        OnHealthChanged?.Invoke();
    }

    public float GetHealth() => currentHealth;

    public void Death()
    {
        OnDeath.Invoke();
        _characterCollider.enabled = false;
    }

    public bool IsDead() => currentHealth <= 0;

    public bool AtFullHealth() => currentHealth == maxHealth;

    public void InstaKill()
    {
        currentHealth = 0;
        OnHealthChanged?.Invoke();
        Death();
    }

    public void InstaHealth()
    {
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke();
        OnHeal?.Invoke();
    }

    [ContextMenu("Resurrect Entity")]
    public void Resurrect(float? life)
    {
        if (IsDead())
        {
            OnResurrect.Invoke();
            currentHealth = life > 0 ? life.Value : maxHealth;
            _characterCollider.enabled = true;
            OnHealthChanged?.Invoke();
            OnHeal?.Invoke();
        }
        else
        {
            InstaHealth();
        }
    }
}