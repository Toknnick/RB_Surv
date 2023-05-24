using System.Collections;
using UnityEngine;

public class EnemyAquariumHead : Enemy
{
    [SerializeField] private GameObject head;
    [Header("Время на то, чтобы отрастить голову")]
    [SerializeField] private float timeToGrow;

    private bool isCanGrowHead = true;
    private bool isHaveHead = true;
    private float nowTime = 0;

    protected override void Start()
    {
        base.Start();
        head.SetActive(true);
        enemy.OnDie += GrowHead;
    }

    protected override void Update()
    {
        nowTime += Time.deltaTime;
        Debug.Log(navMeshAgent.remainingDistance);
        Debug.Log(isSetRunAnimation + "isSetRunAnimation");

        if (isHaveHead)
        {
            if (!isSetRunAnimation && nowTime >= attackRate)
            {
                nowTime = 0;
                enemyAnimationController.SetAttack();
                DealDamage(player);
            }

            if (isSetRunAnimation && navMeshAgent.remainingDistance != 0 && navMeshAgent.remainingDistance <= attackRange)
            {
                isSetRunAnimation = false;
            }
            else if (navMeshAgent.remainingDistance >= attackRange && !isSetRunAnimation || navMeshAgent.remainingDistance == 0)
            {
                if (nowTime >= attackRate / 2)
                {
                    isSetRunAnimation = true;
                    enemyAnimationController.SetRun();
                }
            }

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
