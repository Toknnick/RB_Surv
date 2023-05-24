using System.Collections;
using UnityEngine;

public class EnemyArmoured : Enemy
{
    [SerializeField] protected float timeToPrepareToJump;
    [SerializeField] protected float timeToJump;
    [SerializeField] protected Material changingMaterial;
    [SerializeField] protected MeshRenderer meshRenderer;

    private Material defaultMateril;
    private bool isJumping = false;

    protected override void Start()
    {
        base.Start();
        defaultMateril = meshRenderer.material;
        enemy.OnDie += () => { enemyAnimationController.SetDie(); ; };
    }

    protected override void Update()
    {
        if (!isJumping)
        {
            if (isSetRunAnimation && navMeshAgent.remainingDistance != 0 && navMeshAgent.remainingDistance <= attackRange)
            {
                enemyAnimationController.SetAttack();
                StartCoroutine(Jump(player));
                isSetRunAnimation = false;
            }

            if (isSetRunAnimation)
                base.Update();
        }
    }

    private IEnumerator Jump(EntityPlayer player)
    {
        enemyAnimationController.StopAnim();
        isJumping = true;
        Vector3 position = player.transform.position;
        Vector3 startPosition = transform.position;
        meshRenderer.material = changingMaterial;
        float nowTime = 0;
        yield return new WaitForSecondsRealtime(timeToPrepareToJump);

        while (nowTime < timeToJump)
        {
            transform.position = Vector3.Lerp(startPosition, position, nowTime / timeToJump);
            nowTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSecondsRealtime(timeToJump);
        Collider[] colliders = Physics.OverlapSphere(transform.position, enemy.AttackRange);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                enemy.DealDamage(player, damagePerSec);
                Debug.Log("Дамажит " + gameObject.name);
            }
        }

        isSetRunAnimation = true;
        enemyAnimationController.SetRun();
        isJumping = false;
        meshRenderer.material = defaultMateril;
    }
}
