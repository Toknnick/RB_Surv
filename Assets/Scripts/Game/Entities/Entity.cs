using System;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public event Action OnHealthChanged;

    public float MaxHealth;
    public float Speed;
    public float Damage;
    public float AttackRange;
    public float AttackRate = 1f;

    private float health;

    private void Start()
    {
        health = MaxHealth;
    }

    public float CurrentHealth
    {
        get => health;
        set
        {
            health = value;
            OnHealthChanged?.Invoke();
        }
    }
}
