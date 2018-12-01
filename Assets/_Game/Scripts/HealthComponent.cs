using UnityEngine;

public delegate void DamageHandler(int damage);

public class HealthComponent : MonoBehaviour, IHitTarget
{
    [SerializeField] private int health = 100;

    private int maxHealth;

    public event DamageHandler OnDamageTaken;
    public event DamageHandler OnDeath;

    private void Awake()
    {
        maxHealth = health;
    }

    public void Damage(int damage)
    {
        health = Mathf.Clamp(health - damage, 0, maxHealth);
        OnDamageTaken?.Invoke(damage);
        if (health <= 0)
        {
            OnDeath?.Invoke(damage);
        }
    }

    public float HealthPercentage => (float) maxHealth / health;

    public int Health => health;

    public int MaxHealth => maxHealth;
}