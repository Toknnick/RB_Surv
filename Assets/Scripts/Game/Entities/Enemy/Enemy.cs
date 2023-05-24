using System;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public class Enemy : MonoBehaviour
{
    [SerializeField] protected RectTransform hpBar;
    [SerializeField] protected RectTransform hp;

    protected NavMeshAgent navMeshAgent;
    protected EnemyAnimationController enemyAnimationController;
    protected bool isSetRunAnimation = true;
    protected float damagePerSec;
    protected float attackRange;
    protected float attackRate;
    protected EntityEnemy enemy;
    protected EntityPlayer player;

    public void Off()
    {
        hpBar.gameObject.SetActive(false);
        hp.gameObject.SetActive(false);
        enabled = false;
    }

    protected virtual void Start()
    {
        player = GameManager.instance.Player;
        enemyAnimationController = GetComponent<EnemyAnimationController>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemy = GetComponent<EntityEnemy>();
        navMeshAgent.speed = enemy.Speed;
        damagePerSec = enemy.Damage;
        attackRange = enemy.AttackRange;
        attackRate = enemy.AttackRate;
        navMeshAgent.stoppingDistance = enemy.AttackRange;
        enemy.OnHealthChanged += () => { hp.gameObject.SetActive(enemy.CurrentHealth != enemy.MaxHealth); var a = hpBar.localScale; a.x = enemy.CurrentHealth / enemy.MaxHealth; hpBar.localScale = a; };
    }

    protected virtual void Update()
    {
        navMeshAgent.SetDestination(player.transform.position);
    }

    protected virtual void DealDamage(EntityPlayer player)
    {
        enemy.DealDamage(player, damagePerSec);
        Debug.Log("Дамажит " + gameObject.name);
    }
}
