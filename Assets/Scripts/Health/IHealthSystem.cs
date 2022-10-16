using System;

public interface IHealthSystem
{
    public void Damage(float damage);

    public event Action OnHealthChanged;

    public event Action OnDamage;

    public event Action OnHeal;

    public event Action OnMaxHealthChanged;

    public void Heal(float heal);

    public void SetMaxHealth(float health);

    public float GetMaxHealth();

    public void SetHealth(float health);

    public float GetHealth();

    public void Death();

    public bool IsDead();

    public bool AtFullHealth();

    public void InstaKill();

    public void InstaHealth();

    public void Resurrect(float? health = null);
}