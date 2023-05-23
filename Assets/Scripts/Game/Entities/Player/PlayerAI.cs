using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAI : MonoBehaviour
{
    public event Action<Ability, bool, int> OnLearnedAbility;
    public event Action<Ability, bool, int> OnUpgradedAbility;

    public SpecialAbility SpecialAbility;

    [SerializeField] SphereCollider attackZone;

    [HideInInspector] public List<Ability> Amplifiers;
    [HideInInspector] public List<Ability> Modificators;

    private float damage;
    private EntityPlayer player;
    private EntityEnemy target;
    private float attackRate;
    private float nowTime;

    public void UpgrageAbility(bool isAmplifier, int numberOfAbility)
    {
        if (isAmplifier)
        {
            Amplifiers[numberOfAbility].AddLevel();
            OnUpgradedAbility?.Invoke(Amplifiers[numberOfAbility], true, numberOfAbility);
        }
        else
        {
            Modificators[numberOfAbility].AddLevel();
            OnUpgradedAbility?.Invoke(Modificators[numberOfAbility], false, numberOfAbility);
        }
    }

    public void AddAbillity(Ability abillity, bool isAmplifier)
    {
        int numberOfAbilityInList;

        if (isAmplifier)
        {
            abillity.Use();
            Amplifiers.Add(abillity);
            numberOfAbilityInList = Amplifiers.Count - 1;
        }
        else
        {
            Modificators.Add(abillity);
            numberOfAbilityInList = Modificators.Count - 1;
        }

        OnLearnedAbility?.Invoke(abillity, isAmplifier, numberOfAbilityInList);
    }

    private void Start()
    {
        player = GetComponent<EntityPlayer>();
        damage = player.Damage;
        attackZone.radius = player.AttackRange;
        attackRate = player.AttackRate;
    }

    private void Update()
    {
        nowTime += Time.deltaTime;

        if (nowTime >= attackRate && target != null)
        {
            Debug.Log("Дамажит игрок");
            nowTime = 0;

            if (target.gameObject.GetComponent<EnemyArmoured>() && player.IsMellee == false)
                player.DealDamage(target, damage * 0.2f);
            else
                player.DealDamage(target, damage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (target == null && !other.isTrigger && other.transform.CompareTag("Enemy"))
            target = other.gameObject.GetComponent<EntityEnemy>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (target == null && !other.isTrigger && other.transform.CompareTag("Enemy"))
            target = other.gameObject.GetComponent<EntityEnemy>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.isTrigger && target == other.transform.CompareTag("Enemy"))
            target = null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(gameObject.transform.position, player.AttackRange * 2f);
    }
}
