using System.Collections;
using UnityEngine;

public class EntityPlayer : Entity
{
    public event System.Action OnHealthChange;
    public event System.Action OnDie;

    [Header("�������� ���� ��� ������")]
    [SerializeField] private bool isMellee;
    [Header("���� ���������� �� �����")]
    public float SlopeChance = 10;
    [Header("������������ ���� (� ���������)")]
    [SerializeField] private float reverseDamage = 10;
    [Header("���� ������������ �����")]
    [SerializeField] private float criticalChance = 10;
    [Header("��������� ���������� �����")]
    [SerializeField] private float criticalMultiply = 2.1f;
    [Header("����������� ����")]
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
