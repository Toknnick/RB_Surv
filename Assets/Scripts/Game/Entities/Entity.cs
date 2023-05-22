using System;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public event Action OnHealthChange;
    public event Action OnDie;

    [SerializeField] private bool isMellee;

    public bool IsMellee => isMellee;
    public float SlopeChance = 0;
    public float MaxHealth;
    public float Speed;
    public float DamagePerSec;
    public float AttackRange;
    public float AttackRate = 1f;
    private float Health;

    public float CurrentHealth
    {
        get => Health;
        set
        {
            Health = Mathf.Clamp(value, 0, MaxHealth);
            OnHealthChange?.Invoke();
        }
    }
    public void TakeDamage(float amount)
    {
        if (UnityEngine.Random.Range(0, 100) >= SlopeChance)
            CurrentHealth -= amount;

        if (CurrentHealth <= 0)
            OnDie?.Invoke();
    }

    public void DealDamage(Entity entity, float amount)
    {
        entity.TakeDamage(amount);
    }

    private void Start()
    {
        Health = MaxHealth;
    }
}
