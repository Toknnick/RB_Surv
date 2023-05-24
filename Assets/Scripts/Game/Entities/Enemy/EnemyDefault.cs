using UnityEngine;

public class EnemyDefault : Enemy
{
    private float nowTime = 0;

    protected override void Start()
    {
        base.Start();
        enemy.OnDie += () => { enemyAnimationController.SetDie(); };
    }

    protected override void Update()
    {
        nowTime += Time.deltaTime;

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