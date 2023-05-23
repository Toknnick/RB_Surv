using System.Collections;
using UnityEngine;

public class EntityPlayer : Entity
{
    public event System.Action OnHealthChange;
    public event System.Action OnDie;

    [Header("Параметр типа боя робота")]
    [SerializeField] private bool isMellee;
    [Header("Шанс увернуться от удара")]
    public float SlopeChance = 10;
    [Header("Возвращаемый урон (в процентах)")]
    [SerializeField] private float reverseDamage = 10;
    [Header("Шанс критического урона")]
    [SerializeField] private float criticalChance = 10;
    [Header("Коэфицент увеличения урона")]
    [SerializeField] private float criticalMultiply = 2.1f;
    [Header("Блокируемый урон")]
    [SerializeField] private float blockDamage = 1;

    public bool IsMellee => isMellee;

    public IEnumerator Resurrect()
    {
        CurrentHealth = 99999999;
        yield return new WaitForSecondsRealtime(5);
        CurrentHealth = MaxHealth;
    }


    public void TakeDamage(EntityEnemy damager, float amount)
    {
        amount -= blockDamage;

        if (damager != null)
            damager.TakeDamage(amount * reverseDamage / 100);

        if (Random.Range(0, 100) > SlopeChance)
            CurrentHealth -= amount;

        if (CurrentHealth <= 0)
            OnDie?.Invoke();
    }

    public void DealDamage(EntityEnemy enemy, float amount)
    {
        if (Random.Range(0, 100) <= criticalChance)
            enemy.TakeDamage(amount * criticalMultiply);
        else
            enemy.TakeDamage(amount);
    }
}
