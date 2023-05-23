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

    private float damagePerSec;
    private Entity player;
    private Entity target;
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
        player = GetComponent<Entity>();
        damagePerSec = player.DamagePerSec;
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

            if (target.gameObject.layer == 1 && player.IsMellee == false)
                player.DealDamage(target, damagePerSec * 0.2f);
            else
                player.DealDamage(target, damagePerSec);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (target == null && !other.isTrigger && other.transform.CompareTag("Enemy"))
            target = other.gameObject.GetComponent<Entity>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (target == null && !other.isTrigger && other.transform.CompareTag("Enemy"))
            target = other.gameObject.GetComponent<Entity>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.isTrigger && target == other.transform.CompareTag("Enemy"))
            target = null;
    }
}
