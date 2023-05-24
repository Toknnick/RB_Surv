using System.Collections;
using UnityEngine;

public class EnemyAquariumHead : Enemy
{
    [SerializeField] private GameObject head;
    [Header("Время на то, чтобы отрастить голову")]
    [SerializeField] private float timeToGrow;

    private bool isCanGrowHead = true;
    private bool isHaveHead = true;

    protected override void Start()
    {
        base.Start();
        head.SetActive(true);
        enemy.OnDie += GrowHead;
    }

    protected override void Update()
    {
        if (isHaveHead)
        {
            if (navMeshAgent.isStopped && isSetRunAnimation)
            {
                enemyAnimationController.SetAttack();
                DealDamage(player);
                isSetRunAnimation = false;
            }
            else if (!navMeshAgent.isStopped && !isSetRunAnimation)
            {
                isSetRunAnimation = true;
                enemyAnimationController.SetRun();
                DealDamage(player);
            }

            if (isSetRunAnimation)
                base.Update();
        }
    }

    private void GrowHead()
    {
        head.SetActive(false);
        isHaveHead = false;
        StartCoroutine(TryToGrow());
    }

    private IEnumerator TryToGrow()
    {
        enemyAnimationController.StopAnim();
        enemy.CurrentHealth = enemy.MaxHealth;
        enemy.OnDie -= GrowHead;
        enemy.OnDie += () => { Destroy(gameObject); };
        yield return new WaitForSeconds(timeToGrow);
        Debug.Log("Отрастил голову");
        enemy.OnDie -= () => { Destroy(gameObject); };
        enemy.OnDie += GrowHead;
        enemy.CurrentHealth = enemy.MaxHealth;
        isHaveHead = true;
        head.SetActive(true);
    }
}
