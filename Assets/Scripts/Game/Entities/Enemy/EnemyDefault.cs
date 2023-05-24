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

        if (isSetRunAnimation && navMeshAgent.remainingDistance != 0 && navMeshAgent.remainingDistance <= attackRange)
        {
            if (nowTime >= attackRate)
            {
                nowTime = 0;
                enemyAnimationController.SetAttack();
                DealDamage(player);
                isSetRunAnimation = false;
            }
        }
        else if (navMeshAgent.remainingDistance >= attackRange && !isSetRunAnimation)
        {
            if (nowTime >= attackRate / 2)
            {
                isSetRunAnimation = true;
                enemyAnimationController.SetRun();
                DealDamage(player);
            }

        }

        base.Update();
    }
}